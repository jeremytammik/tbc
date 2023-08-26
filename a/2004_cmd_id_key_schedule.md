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

- Typography-Fell-Types-font
  https://www.linyangchen.com/Typography-Fell-Types-font
  A 17th-century font in a 21st-century thesis
  special guy: https://www.linyangchen.com/

twitter:

AI generates 3D model from 2D drawings, GasTools, key schedule browser API and @AutodeskRevit command id list for the #RevitAPI  #BIM @DynamoBIM @AutodeskAPS https://autode.sk/rvt_cmd_ids

A nice, varied bouquet of topics
&ndash; GasTools
&ndash; Key schedule browser API
&ndash; Revit command id list
&ndash; AI generates 3D model from 2D drawings
&ndash; Careful with infant screen use
&ndash; Baroque typography in the digital world...

linkedin:

AI generates 3D model from 2D drawings, GasTools, key schedule browser API and Revit command id list for the #RevitAPI

https://autode.sk/rvt_cmd_ids

A nice, varied bouquet of topics

- GasTools
- Key schedule browser API
- Revit command id list
- AI generates 3D model from 2D drawings
- Careful with infant screen use
- Baroque typography in the digital world...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### GasTools, Cmd Ids, Key Schedule et al

Today we present a nice, varied bouquet of interesting topics:

- [GasTools](#2)
- [Key schedule browser API](#3)
- [Revit command id list](#4)
- [AI generates 3D model from 2D drawings](#5)
- [Careful with infant screen use](#6)
- [Baroque typography in the digital world](#7)

####<a name="2"></a> GasTools

Gastón Balparda Corsi, Architect and Project Developer at [/slantis](https://slantis.com/),
announced his [GasTools](https://github.com/GastonBC/GasTools/wiki), saying:

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
- Outline Elevations &ndash; If you've ever done elevations, you are used to making fill regions around the enclosing walls and giving it a pen thickness. This tool takes the crop region extent and overrides its thickness to the desired number.
- Auto Dimension Grids &ndash; Dimension your selected grids automatically to the top and the left.
- Smart Grid Bubbles &ndash; Turn the top and left grid bubbles on.
- Set by Index &ndash; Create a sheet set by selecting a schedule like the one in your cover sheet instead of checking sheet by sheet.


####<a name="3"></a> Key Schedule Browser API

Jacob Small, Autodesk Implementation Consultant, chipped in with some new advice on how to control the hierarchy of the schedule in the project browser in the thread
on [Key Schedule + Revit API](https://forums.autodesk.com/t5/revit-api-forum/key-schedule-revit-api/m-p/12143666):

**Question:** I have previously asked about using Revit API to create a Key Schedule and add bunch of new parameters to it.
That question is still out here somewhere so I am not going to repeat it here.
Instead, I just manually added bunch of parameters to Key Schedule and moved on to the next task.
I was able to access Data Table and add more rows to the data.
What I am trying to do now, is to fill the cells with data.
Obviously, there is no clear method for that.
Hence my question: How does one, using API, fill in the Key Schedule data?
Any ideas/suggestions are welcome.

<center>
<img src="img/key_schedule_api.jpg" alt="Key schedule API" title="Key schedule API" width="400"/> <!-- Pixel Height: 460 Pixel Width: 460 -->
</center>

**Answer:** A keyschedule behaves in most ways just like a normal schedule.
The most important similarity being that they show a collection of elements.
Confusion can arise when thinking about the elements in the keyschedule, because they do not physically exist in the Revit model.
I like to think of them as 'ghost elements', who's image (= set of parameter values) can be imprinted on real elements.

This being the case, the elements in the keyschedule can be simply retrieved by using a `FilteredElementCollector` with the `KeyScheduleID` as an argument.
Parameters shown in the keyschedule can then be retrieved and set in the elements themselves.
The newly set parameter values will then of course show up in the key schedule.

**Response:** This solution works pretty well!

Now just to make it clear for everyone here's what really happens:

Each row in a Key Schedule is an Element that contains a set of Parameters.
Parameters represent each column of the schedule.

We can collect all "rows" (elements) just by doing this:

<pre class="prettyprint">
  elements = FilteredElementCollector(doc, viewSchedule.Id).ToElements()
</pre>

Then we can query each element that is being returns for parameters:

<pre class="prettyprint">
  params = []
  for i in elements:
    params.append(i.Parameters)
</pre>

Once you have the parameters, you can just write values to them and they will appear in a schedule.

Not all parameters represent a field in the key schedule, though.
But that can be easily filtered out by checking the parameter's definition name toward schedulable fields.

Maybe the Revit API SDK team can include some of the code in the schedule samples, before a more intrinsic method for reaching this goal being created.

**Question:** Which parameter is responsible of defining the hierarchy of the schedule in project browser, at the question marks in this picture?

<center>
<img src="img/key_schedule_in_browser.jpg" alt="Key schedule in browser" title="Key schedule in browser" width="390"/> <!-- Pixel Height: 306 Pixel Width: 390 -->
</center>

No elements are collected by this code:

<pre class="prettyprint">
  Category category = Category.GetCategory(doc, BuiltInCategory.OST_Rooms);
  viewSchedule = ViewSchedule.CreateKeySchedule(doc, category.Id);
  FilteredElementCollector elementCollector = new FilteredElementCollector(doc, viewSchedule.Id);
  IList&lt;Element&gt; rows = elementCollector.ToElements();
</pre>

**Answer:** This is something I've taken a stab at in the past for a Dynamo project, so I'll weigh in.

First thing to know: the project browser is a view of the model itself and can be organised in different ways per project.
As always, you want to explore how things are in the UI before attempting to automate it, so first check info on managing it manually:
[Organizing the Project Browser](https://help.autodesk.com/view/RVT/2024/ENU/?guid=GUID-96F9CDB5-C46D-4597-943B-DF231E8EC688)

From there we can look at managing it via the API.
For that there is the
handy [`BrowserOrganization` class](https://www.revitapidocs.com/2024/4fd57c3f-6127-efd9-f79e-70ad3e5dc1cc.htm).

So, while we could manage the setup, we don't know how your browser is organized on a given project (and if your code is intended to scale across orgs/projects you can't assume there is a single setup).
As such, you likely want to start off by looking at
[the GetCurrentBrowserOrganizationForSchedules method](https://www.revitapidocs.com/2024/b32365b9-54d0-08b3-ee34-4a2cde7fa1d8.htm)

Good luck! &nbsp;   :-)

Mille grazie, Jacob!

####<a name="4"></a> Revit Command Id List

One approach to retrieve a list of all [Revit command ids](https://forums.autodesk.com/t5/revit-api-forum/revit-command-ids/m-p/12154992):

> I found that exporting the keyboard shortcuts XML file gives you a list of all command names and ids (Revit 2022.1.3, I might add):

<center>
<img src="img/cmd_id_in_shortcut_xml.png" alt="Command id in shortcuts XML" title="Command id in shortcuts XML" width="1000"/> <!-- Pixel Height: 458 Pixel Width: 2,017 -->
</center>

####<a name="5"></a> AI Generates 3D Model from 2D Drawings

Of special interest to CAD and BIM modelers, a new method to generate a 3D CAD model from 2D line drawings of three orthographic views is described in
[PlankAssembly: robust 3D reconstruction from three orthographic views with learnt shape programs](https://manycore-research.github.io/PlankAssembly/).

####<a name="6"></a> Careful with Infant Screen Use

Vital and life-changing effects are seen to correlate with infant interaction with digital devices:
according to a study by the National University of Singapore and several other research labs,
[infant screen use leads to reduced cognitive skills at age 9](https://www.uxtigers.com/post/infant-screen-use).

####<a name="7"></a> Baroque Typography in the Digital World

To close on a pleasant note, I very much enjoyed the explanation (and good taste) by Lin Yangchen in his article on
using [a 17th-century font in a 21st-century thesis](https://www.linyangchen.com/Typography-Fell-Types-font).
He seems like a special person in many ways, and I found his entire web site
[linyangchen.com](https://www.linyangchen.com/) very interesting indeed.

