<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

https://github.com/GastonBC/GasTools/wiki

- Key Schedule + Revit API
  https://forums.autodesk.com/t5/revit-api-forum/key-schedule-revit-api/m-p/12143666
  control hierarchy of the schedule in project browser

- Revit Command Ids
  https://forums.autodesk.com/t5/revit-api-forum/revit-command-ids/m-p/12154992
  I was looking for the same thing and found that exporting the keyboard shortcuts xml file gives you a list of all command names and ids.
  Yes it does (Revit 2022.1.3 I might add)
  cmd_id_in_shortcut_xml.png Pixel Height: 458 Pixel Width: 2,017

- Revit Command Ids
  How to check the exisiting of ribbon tab and ribbon panel? And how to get them?
  https://forums.autodesk.com/t5/revit-api-forum/how-to-check-the-exisiting-of-ribbon-tab-and-ribbon-panel-and/m-p/6859336
  http://forums.autodesk.com/t5/revit-api-forum/how-to-check-the-exisiting-of-ribbon-tab-and-ribbon-panel-and/m-p/6859336

- Typography-Fell-Types-font
  https://www.linyangchen.com/Typography-Fell-Types-font
  A 17th-century font in a 21st-century thesis
  special guy: https://www.linyangchen.com/

twitter:

 @AutodeskAPS  @AutodeskRevit #RevitAPI  #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Key Schedule


####<a name="2"></a> GasTools

Gastón Balparda CorsiGastón Balparda Corsi, Architect and Project Developer at [/slantis](https://slantis.com/) announced
his [GasTools](https://github.com/GastonBC/GasTools/wiki), saying:

> I'm lazy.
When I notice I'm doing something repetitive or boring I default to look for a better way to do stuff.
That mentality allowed me to make the jump from AutoCAD to Revit early and learn a couple of programming languages to help me on my everyday tasks.
The same laziness led me to mix the two and develop my own addins to help my team and I be more efficient with our time.
I received positive feedback, so I decided to share them and save more time in combined hours.
I recommend you start by checking what Match Grid Extents and Outline Elevations do.
You will find the downloads and a brief explanation for each tool in my GitHub account.
Look for a tab in Revit called "Gas Tools" (subject to change).

I'm open to questions and suggestions so don't be afraid to hit me up! These are all independent to install so just download the ones you want. There is some nuance to the installation but nothing terrible.

- [GasTools](https://github.com/GastonBC/GasTools/wiki) &ndash; tools I've developed over the years to make my life easier using Revit.
They automate repetitive tasks or provide a better way of visualizing the information, like the location of linked and imported CADs, legends. Examples of use are in the referenced links.
- Match Grid Extents &ndash; This tool lets you copy the layout of the grids in a sheet (for example the elbows, 2D extents and bubble visibility) and transfer it to another view. It also reveals and hides grids according to the view chosen as template.
- Outline Elevations &ndash; If you've ever done elevations, you are used to making fill regions around the enclosing walls and giving it a pen thickness. This tool takes the crop region extent and overrides it's thickness to the desired number.
- Auto Dimension Grids &ndash; Dimension your selected grids automatically to the top and the left.
- Smart Grid Bubbles &ndash; Turn the top and left grid bubbles on.
- Set by Index &ndash; Create a sheet set by selecting an a schedule like the one in your cover sheet instead of checking sheet by sheet.


####<a name="3"></a> Key Schedule

Jacob Small chipped in with some new insight on how to control the hierarchy of the schedule in the project browser in the thread
on [Key Schedule + Revit API](https://forums.autodesk.com/t5/revit-api-forum/key-schedule-revit-api/m-p/12143666):

**Question:** I have previously asked about using Revit API to create a Key Schedule and add bunch of new parameters to it. That questions is still out here somewhere so I am not going to repeat it here. Instead I just manually added bunch of parameters to Key Schedule and moved on to the next task.
I was able to access Data Table and add more rows to the data.
What I am trying to do now, is to fill the cells with data.
Obviously, there is no clear method for that.
Hence my question: How does one, using API, fill in the Key Schedule data?
Any ideas/suggestions are welcome.

<center>
<img src="img/key_schedule_api.jpg" alt="" title="" width="100"/> <!-- Pixel Height: 460 Pixel Width: 460 -->
</center>


Snooping around in the Revit API help file RevitAPI.chm, I see the ViewSchedule.KeyScheduleParameterName property that provides the name of the parameter for choosing one of the keys in a key schedule.

Maybe you should snoop around a bit yourself as well.

Cheers,

Jeremy


Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Tags (0)
Add tags
Report
MESSAGE 3 OF 14
sobon.konrad
  Advocate sobon.konrad  in reply to: jeremytammik
‎2014-12-23 10:22 AM
Jeremy,

Yes, I saw that too. I guess I fail to understand how to use KeyScheduleParameterName to select a Key and then write some information to it. Care for an example? I am sorry, if my question is a bit on a noob side, but I am not an experienced Revit API programmer, and you are far too nice of a person (we met before :-)) to give such dismissing answer. Also, Revit API is not the easiest thing on earth so please give me some leeway if my questions are not exactly challanging. Trust me, they are challanging enough for me.

Thank you,

Konrad
Tags (0)
Add tags
Report
MESSAGE 4 OF 14
jeremytammik
  Autodesk jeremytammik  in reply to: sobon.konrad
‎2014-12-23 10:50 AM
Dear Konrad,

Thank you for your appreciation.

My answers are often evasive because I am evading the question.

I cannot answer this one myself without doing research.

Let's wait until next year.

If it is not solved by then, I will either take a closer look or pass it on to the development team.

Thank you for your understanding.

Cheers,

Jeremy


Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Tags (0)
Add tags
Report
MESSAGE 5 OF 14
PaulusPresent_BB
  Enthusiast PaulusPresent_BB  in reply to: jeremytammik
‎2015-07-10 12:19 AM

Dear Jeremy and Konrad,

Maybe I can be off help. I just had to deal with this problem as well and wanted to share my findings with you.

A keyschedule behaves in most ways just like a normal schedule. The most important similarity being that they show a collection of elements.
Confusion can arise when thinking about the elements in the keyschedule, because they do not physically exist in the revit model. I like to think of them as 'ghost elements', who's image (= set of parameter values) can be imprinted on real elements.

This being the case, the elements in the keyschedule can be simply retrieved by using a FilteredElementCollector with the KeyScheduleID as an argument. Parameters shown in the keyschedule can then be retrieved and set in the elements themselves. The newly set parametervalues will then ofcourse show up in the keyschedule.

Kind regards and my excuses if this post is too obvious.
I hope someone still has some use of this info.

Paulus
Tags (0)
Add tags
Report
MESSAGE 6 OF 14
JeffYao28
  Enthusiast JeffYao28  in reply to: PaulusPresent_BB
‎2016-04-22 10:01 AM
This solution works pretty well!
Tags (0)
Add tags
Report
MESSAGE 7 OF 14
sobon.konrad
  Advocate sobon.konrad  in reply to: JeffYao28
‎2016-04-22 12:24 PM
Ah! I see. Now just to make it clear for everyone here's what reall happens:

Each row in a Key Schedule is an Element that contains a set of Parameters. Parameters represent each column of the schedule.

We can collect all "rows" (elements) just by doing this:

elements = FilteredElementCollector(doc, viewSchedule.Id).ToElements()
Then we can query each element that is being returns for parameters:

params = []
for i in elements:
   params.append(i.Parameters)
Once you have the parameters you can just write values to them and they will appear in a schedule.

Cheers!
Tags (0)
Add tags
Report
MESSAGE 8 OF 14
JeffYao28
  Enthusiast JeffYao28  in reply to: sobon.konrad
‎2016-04-22 12:42 PM
Thank you for making that clear!
Not all parameters are representing a field in the key schedule, though. But that can be easily filtered out by checking the parameter's definition name toward scheduleable fields.
Maybe the Revit API SDK team can include some of the code in the schedule samples, before a more intrincit method for reaching this goal being created.
Tags (0)
Add tags
Report
MESSAGE 9 OF 14
hishamodish
  Community Visitor hishamodish  in reply to: sobon.konrad
‎2017-05-02 08:34 AM
Hi Konrad,
Would you mind sharing with me your code?
I’m new to the API but being able to stich codes that I’m finding here and there. However this one I’m failing so far. I’m able to filter my key schedule, able to get to the column I need. BUT NOT able to read the data of that column.
Any help you can provide is much appreciated.
Thanks
hisham
Tags (0)
Add tags
Report
MESSAGE 10 OF 14
ghaith.tishNS5ZX
  Explorer ghaith.tishNS5ZX  in reply to: sobon.konrad
‎2023-08-02 01:45 AM

Which parameter is responsible of defining the herarchy of the scheduel in project browser ?.
Where the question marks in the the loaded picture are.

e.JPG
29 KB

Tags (0)
Add tags
Report
MESSAGE 11 OF 14
ghaith.tishNS5ZX
  Explorer ghaith.tishNS5ZX  in reply to: sobon.konrad
‎2023-08-02 01:46 AM

e.JPG
 
Tags (0)
Add tags
Report
MESSAGE 12 OF 14
ghaith.tishNS5ZX
  Explorer ghaith.tishNS5ZX  in reply to: PaulusPresent_BB
‎2023-08-02 02:30 AM
I have used the concept but there are no elements collected in the

Category category = Category.GetCategory(doc, BuiltInCategory.OST_Rooms);
viewSchedule = ViewSchedule.CreateKeySchedule(doc, category.Id);
FilteredElementCollector elementCollector = new FilteredElementCollector(doc, viewSchedule.Id);
IList<Element> rows = elementCollector.ToElements();


Tags (0)
Add tags
Report
MESSAGE 13 OF 14
jeremy.tammik
  Autodesk jeremy.tammik  in reply to: ghaith.tishNS5ZX
‎2023-08-02 02:33 AM

I asked the development team for you.

Jeremy Tammik,  Developer Advocacy and Support, The Building Coder, Autodesk Developer Network, ADN Open
Tags (0)
Add tags
Report
MESSAGE 14 OF 14
jacob.small
  Autodesk Support jacob.small  in reply to: ghaith.tishNS5ZX
‎2023-08-02 03:26 AM

Not a Revit developer, but this is something I've taken a stab at in the past for a Dynamo project, so I'll weigh in.

First thing to know: the project browser is a view of the model itself, and can be organized in different ways per project. As always, you want to explore how things are in the UI before attempting to automate it, so here's some info on managing it manually:
https://help.autodesk.com/view/RVT/2022/ENU/?guid=GUID-96F9CDB5-C46D-4597-943B-DF231E8EC688

From there we can look at managing it via the API. For that there is a handy class:
https://www.revitapidocs.com/2024/4fd57c3f-6127-efd9-f79e-70ad3e5dc1cc.htm

So while we could manage the setup, we don't know how your browser is organized on a given project (and if your code is intended to scale across orgs/projects you can't assume there is a single setup). As such you likely want to have a look at this method to start: https://www.revitapidocs.com/2024/b32365b9-54d0-08b3-ee34-4a2cde7fa1d8.htm

Good luck. 🙂


####<a name="3"></a>


<pre class="prettyprint">


</pre>



**Answer:**

**Response:**

####<a name="4"></a> Revit Command Ids

- Revit Command Ids
https://forums.autodesk.com/t5/revit-api-forum/revit-command-ids/m-p/12154992
I was looking for the same thing and found that exporting the keyboard shortcuts xml file gives you a list of all command names and ids.
Yes it does (Revit 2022.1.3 I might add)
cmd_id_in_shortcut_xml.png Pixel Height: 458 Pixel Width: 2,017

How to check the exisiting of ribbon tab and ribbon panel? And how to get them?
https://forums.autodesk.com/t5/revit-api-forum/how-to-check-the-exisiting-of-ribbon-tab-and-ribbon-panel-and/m-p/6859336
http://forums.autodesk.com/t5/revit-api-forum/how-to-check-the-exisiting-of-ribbon-tab-and-ribbon-panel-and/m-p/6859336

####<a name="5"></a> AI-Supported3D View Generation from 2D Drawings

A new method to generate a 3D CAD model from 2D line drawings of three orthographic views:
[PlankAssembly: Robust 3D Reconstruction from Three Orthographic Views with Learnt Shape Programs](https://manycore-research.github.io/PlankAssembly/)


####<a name="6"></a> Careful with Infant Screen Use

According to a study by ther National University of Singapore and several other research labs,
[Infant Screen Use Leads to Reduced Cognitive Skills at Age 9](https://www.uxtigers.com/post/infant-screen-use).

