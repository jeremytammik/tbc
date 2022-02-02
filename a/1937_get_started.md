<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


- email exchange with carol
  
- Newtonsoft.Json.dll version conflict
  https://autodesk.slack.com/archives/C0SR6NAP8/p1641277768200900
  Naveen Kumar T Today at 07:29
  Stone Shi  4 hours ago

- python dynamo and you have the source code available for all
  10 AMAZING PYREVIT FEATURES TO SAVE INSANE AMOUNTS OF TIME
  January 15, 2020 Nicolas Catellier
  https://revitpure.com/blog/10-amazing-pyrevit-features-to-save-insane-amounts-of-time

- gulshannegi94 updated the thread on 
  Books or other sources to learn Python to be used in Revit Dynamo
  https://forums.autodesk.com/t5/revit-api-forum/books-or-other-sources-to-learn-python-to-be-used-in-revit/m-p/10913051

- Powerful: [Are You Lost in the World Like Me?](https://youtu.be/PJXCJOgPgZI),
  an animated two-and-a-half-minute short film by [Steve Cutts](https://www.stevecutts.com)

twitter:

 the #RevitAPI FormulaManager @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon
 
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

### Getting Started Once Again

Welcoming my new colleague Carol leads to a renewed look at getting started:

- [Access permission to load my first add-in](#2)
- [My first Revit plug-in todo](#3)
- [Books on Python for Revit Dynamo](#4)
- [pyRevit saves insane amounts of time](#5)
- [Newtonsoft Json.dll version conflict](#6)
- [Lost in the World by Steve Cutts](#7)

####<a name="2"></a> Access Permission to Load my First Add-In

I probably pointed beginners to
the [Revit API getting started material](https://thebuildingcoder.typepad.com/blog/about-the-author.html#2) several
thousand times already, and sincerely hope that it provides a couple of useful pointers for them.

Still, people keep running into problems anyway.

A colleague hit another unexpected obstable last week:

**Question:** I dived into the Getting Started with Revit platform API, following the DevTV tutorial by Augusto Goncalves.
None of my commands appear on the Revit UI &gt; Add Ins &gt; external commands. 

**Answer:** One thing you ought to read is the introductory section of
the [Revit API developers guide](https://help.autodesk.com/view/RVT/2022/ENU/?guid=Revit_API_Revit_API_Developers_Guide_html).
It tells you exactly what to do to install and launch your add-in.
It is shocking if that information is not clear and does not work in the tutorial, though.
Thank you for bringing it up!

Installing a Revit add-in is really simple, but people run into difficulties like you describe anyway.
  
There are only two relevant components:
 
- Add-in manifest file `*.addin`
- .NET class library assembly `DLL`
 
These are the important steps:
 
- The DLL must implement `IExternalCommand`; that means, it must implement the `Execute` method.
- The add-in manifest must point to the DLL and must be placed in the Revit Add-Ins folder for Revit to find and load it.
- If the DLL and add-in manifest both reside in the Revit `AddIns` folder, the full DLL path can be omitted; otherwise it must be specified.
 
That is really all.
 
There are thousands of places explaining it; they all say the same thing.
 
Good luck and lots of fun with the Revit API :-)

**Response:** I have not had any luck since yesterday about my add-in not appearing in the Revit external commands.

I have carefully structured my code correctly.
The add-in manifest file is pointing to my project `.dll` file.
My project class explicitly implements the `IExternalCommand` interface and fires up the `Execute` method just fine.
I don't understand what the issue could be.

**Update:** I managed to debug my code.
Kindly, ignore previous message. 

The location of my manifest add-in file was locked.
I guess that happened when my account was set up.
The location needed permission to be accessed.
This path: 

- C:\ProgramData\Autodesk\Revit\Addins\2022\

I utilised the try and catch exception to see the issue.

Once I gave access permission, the add-in file is now visible; it worked!

####<a name="3"></a> My First Revit Plug-in Todo

The [My First Revit Plug-in tutorial](https://knowledge.autodesk.com/search-result/caas/simplecontent/content/my-first-revit-plug-overview.html) available from
the [Revit Developer Center](https://www.autodesk.com/developer-network/platform-technologies/revit) needs
an overhaul, as 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Revit Add-ins Tutorial needs an update for version 2021.1](https://forums.autodesk.com/t5/revit-api-forum/revit-add-ins-tutorial-needs-an-update-for-version-2021-1/td-p/10905890)
points out.

Our new team member [Carol Gitonga](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/11981988) is
just getting started with the Revit API herself and will very kindly take a look at it.

Many thanks and a warm welcome to Carol!

####<a name="4"></a> Books on Python for Revit Dynamo

Discussing another area to get started in,
Gulshan [gulshannegi94](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/11923128) Negi updated the thread
on [books or other resources to learn Python for Revit Dynamo](https://forums.autodesk.com/t5/revit-api-forum/books-or-other-sources-to-learn-python-to-be-used-in-revit/m-p/10913051):

Best ones are:

- Learn Python the Hard Way by Zed Shaw a very popular author and a must-have book for any python student. In Learn Python the Hard Way, you'll learn Python by working through 52 brilliantly crafted exercises
- Python For Data Analysis The book is a complete guide on processing, cleaning, influencing and gathering of data in Python. It is made for the area of data intensive applications and provides an excellent introduction on data analysis issues. It is the best source for understanding the various tools.
- Python, In A Nutshell provides an easy guide on Python programming language. It is a perfect source when it comes to areas like the official library and language references. This book is to be read by those who already have their fundamentals on Python strong. It deals with many advanced and complicated areas regarding the subject.
- Violent Python: A Cookbook For Hackers, Forensic Analysts, Penetration Testers Written by TJ O’Connor, this book is an introductory level book on Python programming language that provides a clear-cut understanding. This book will teach you to forge your own weapons using the Python programming language instead of relying on another attacker’s tools. It is the best book to read when it comes to security concepts and deals with forensics, tool integration for complicated protocols like SMB. It also demonstrates how to write Python scripts to automate large-scale network attacks, extract metadata, and investigate forensic artifacts. The book is apt to be used by those programmers who already have a good understanding of the Python language.
- Python Machine Learning Unlock deeper insights into Machine Leaning with this vital guide to cutting-edge predictive analytics about This book leverages Python's most powerful open-source libraries for deep learning, data wrangling, and data visualization. Learn effective strategies and best practices to improve and optimize machine learning systems and algorithms. Ask and answer tough questions of your data with robust statistical models, built for a range of datasets.

For more best Python book recommendations, check out
the [10 Best Python Books for Beginners & Advanced Programmers](https://learndunia.com/best-python-books).

####<a name="5"></a> pyRevit Saves Insane Amounts of Time

Talking about Python and Dynamo in Revit, Nicolas Catellier highlights
[10 amazing pyRevit features to save insane amounts of time](https://revitpure.com/blog/10-amazing-pyrevit-features-to-save-insane-amounts-of-time).
As always, one of the best aspects of this is that it is all open source, so you access to source code for all the functionality presented.

####<a name="6"></a> Newtonsoft Json.dll Version Conflict

**Question:** A developer reported problems with their Revit add-ins.
They make no use of any web services or BIM 360.
Still, they cause problems with BIM360 under certain circumstances:

- In Revit 2019 everything works as expected, the client can see their BIM 360 folders and files.
- In Revit 2020 the client cannot access or see their BIM 360 folders via Revit, but they can see their organization.
- The same is true for Revit 2021 and Revit 2022.
- If the apps are uninstalled, Revit 2020 works as expected.
- If the client upgrades their BIM 360 project to 2020 then Revit 2020 works as expected.
- When we test on different machines, even with no apps installed, Revit 2021 and Revit 2022 doesn't even show the BIM360 organization.

**Answer:** Probably some DLL in the customer’s add-on conflicts with Revit’s.

Does it by any chance use `Newtonsoft.Json.dll`?

After some analysis, I can see it does indeed.

The add-in uses `Newtonsoft.Json.dll` version 13.0.1, two major versions newer than the one shipped with Revit 2021, which is version 11.0.2.

Probably they will have to downgrade the DLL.
That is easy, if the app doesn’t consume any features only available only in versions 12 or 13.

They also need to make sure not to explicitly load the DLL (and any other DLLs), e.g., using `Assembly.LoadFromFile`.

In another case, a third-party add-in used the same version of Newtonsoft as Revit but explicitly loaded the DLL; this caused similar issues to Revit core functionalities.

Yet another example of DLL hell resolved.

####<a name="7"></a> Lost in the World by Steve Cutts 

To wrap up, a little non-programming topic:
I liked the animated two-and-a-half-minute short film [Are You Lost in the World Like Me?](https://youtu.be/PJXCJOgPgZI)
by [Steve Cutts](https://www.stevecutts.com) very much,
leading me to check out several of [his other animations](https://www.stevecutts.com/animation.html).

<center>
<img src="img/stevecutts.png" alt="Steve Cutts" title="Steve Cutts" width="260"/> <!-- 519 -->
</center>
