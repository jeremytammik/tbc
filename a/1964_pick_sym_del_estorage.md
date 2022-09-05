<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- unload ifc links to delete extensible storage?
  https://forums.autodesk.com/t5/revit-api-forum/not-able-to-delete-extensible-storage-schema/m-p/11397400#M65728

- solution by rpthomas with some important UI design and selection advice in general
  Pick FamilySymbol from Project Browser?
  https://forums.autodesk.com/t5/revit-api-forum/pick-familysymbol-from-project-browser/td-p/11390552

twitter:

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

###

**Question:** 

**Answer:** 

<center>
<img src="img/.png" alt="" title="" width="400"/> <!-- 622 x 485 -->
</center>

<pre class="code">
</pre>

Thank you ... for the useful sample code and explanation!

####<a name="2"></a> Unload IFC Links to Delete Extensible Storage

Several developers reported problems deleting extensible storage.

A new contribution to 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
[not able to delete extensible storage schema](https://forums.autodesk.com/t5/revit-api-forum/not-able-to-delete-extensible-storage-schema/m-p/11397400) reports
that unloading IFC links may help:

> I created a very small
example [Example_RemoveSchemas.zip](zip/Example_RemoveSchemas.zip) with
an IFC link with a few walls created in Revit 2020.
The file contains 4 macros:

>    - Create example schema
- Remove example schema
- List all schemata
- Remove all schemata

> In Revit, 2020 I am not able to remove any schemata when the IFC link is loaded.

Many thanks to Marek @Songohu_85 for testing and reporting this.

I wonder whether it will help others resolve similar issues.

####<a name="3"></a> Picking, Selection and UI Design

Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
shares some mimportant advice on Revit add-in UI design and selection in general in his answer on
how to [pick `FamilySymbol` from project browser](https://forums.autodesk.com/t5/revit-api-forum/pick-familysymbol-from-project-browser/td-p/11390552):

**Question:** I want my user to select a FamilySymbol that's already loaded from the UI (more specifically, the Project Browser) that I then further process in my own code. I'm aware that there are different methods for filtering the already existing custom families/loading families from file and so on, but is there a way to prompt the user to pick one from the project browser? Ideally, it would NOT require the user to make conscious decisions on where to click before starting the add-in.

Cheers

 Solved by RPTHOMAS108. Go to Solution.

Report
Labels (2)
FamilySymbol PickObject
6 REPLIES
MESSAGE 2 OF 7
Yien_Chao
 Advisor Yien_Chao in reply to: grubdex
‎08-30-2022 09:50 AM 
hi

you can ask users to select element(s) instance from their view then manage to get the FamilySymbol, but i don't think it's possible from the browser.

Report
MESSAGE 3 OF 7
RPTHOMAS108
 Mentor RPTHOMAS108 in reply to: grubdex
‎08-30-2022 04:29 PM 
It's not the usual workflow but now in 2023 you have the selection changed event which also applies to project browser. The below prompts user to select from browser before adding a selection changed event.

The handler filters for the class of FamilySymbol* and shows a dialogue if the result yields more than 0 results.

Note that not every thing in the project browser is a FamilySymbol so ElementType class will cover more aspects such as system families.

You would have to continue execution in the handler and perhaps raise an external event to get an editable document status (I suspect the selection changed event doesn't give you that).

You can also use e.GetDocument below instead of casting sender 's' to UIApplication to get the Document.

Public Function Obj_220831a(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result
        Dim IntUIApp As UIApplication = commandData.Application
        Dim IntUIDoc As UIDocument = commandData.Application.ActiveUIDocument
        Dim IntDoc As Document = IntUIDoc.Document

        TaskDialog.Show("Select", "Pick family symbol from project browser")
        AddHandler IntUIApp.SelectionChanged, AddressOf SelectionChanged

        Return Result.Succeeded
    End Function

    Public Sub SelectionChanged(s As Object, e As Autodesk.Revit.UI.Events.SelectionChangedEventArgs)
        Dim UIapp As UIApplication = s
        Dim J As ISet(Of ElementId) = e.GetSelectedElements
        Dim J0 As List(Of ElementId) = J.ToList

        Dim FEC As New FilteredElementCollector(UIapp.ActiveUIDocument.Document, J0)
        Dim F0 As New ElementClassFilter(GetType(FamilySymbol))
        Dim Els As List(Of Element) = FEC.WherePasses(F0).ToElements

        If Els.Count > 0 Then
            TaskDialog.Show("Selection", Els(0).Name)
            RemoveHandler UIapp.SelectionChanged, AddressOf SelectionChanged
        End If
    End Sub

Report
MESSAGE 4 OF 7
grubdex
 New Member grubdex in reply to: RPTHOMAS108
‎08-31-2022 01:17 AM 
Thanks for your reply. In your opinion, what would be a workflow that is more common?

Report
MESSAGE 5 OF 7
RPTHOMAS108
 Mentor RPTHOMAS108 in reply to: grubdex
‎08-31-2022 05:51 AM 
It really depends on what your add-in is being used for i.e is it modal or modeless interaction is it working with a few categories of elements or many?

It is often the case you have to create dialogues that replicate the ones Revit inherently has which is a bit annoying but doesn't take that long with WPF for something such as FamilySymbol selection. That would allow a modal interaction.

You aim to stay within the IExternalCommand context in a modal way to start with but if that isn't possible then you have to get back into a similar context via raising ExternalEvents from modeless forms etc. There are also other methods of obtaining a valid context to work with.

Report
MESSAGE 6 OF 7
grubdex
 New Member grubdex in reply to: RPTHOMAS108
‎09-01-2022 06:41 AM 
Thanks for the answer!

Do you happen to have any pointers or examples of how to develop modeless forms with the Revit API by chance? Would be much appreciated, thanks!

Report
MESSAGE 7 OF 7
RPTHOMAS108
 Mentor RPTHOMAS108 in reply to: grubdex
‎09-01-2022 09:13 AM 
There are samples in the SDK:

...\Samples\ModelessDialog\ModelessForm_ExternalEvent

It is for Windows Forms but same approach would be used for WPF.

####<a name="5"></a> Feedback is Always a Great Gift

thank you for the important impulse.
 
In case anyone is interested in going one step further in the direction of listening and basically all kinds of human communication and interaction, I can highly recommend a book by Scott Peck on community building:
 
https://en.wikipedia.org/wiki/M._Scott_Peck#Community_building
 
one of the best books I have ever read.
 
And its main focus is on really truly listening.
 
After we both read the book, Moni and I participated in a weekend workshop to practice community building with 26 people we didn't know, supported by 5 facilitators. The facilitators basically did nothing but repeat certain very simple and basic communication recommendations, such as:
 
I really listen
I speak only about myself
I speak only when truly moved to speak
 
Highly recommended practices for all human interaction, especially in teams, relationships, families, politics, enterprises, projects…
 
Happy sunday and good listening to all,
 
cheers,
 
jeremy
 
From: Jim Quanci <jim.quanci@autodesk.com>
Date: Sunday, 4 September 2022 at 11:23
To: DAS Worldwide <das-worldwide@autodesk.com>
Subject: FW: 🎁 Axios Finish Line: Taking the gift

Thought some of you might find the below piece on “feedback” interesting.
I actually believe most of our team is pretty good at accepting feedback… but I know I can at times “not full embrace feedback enough”.
This article is a nice reminder.  To some degree, its about receiving feedback with humility. 😉
 
Cheers,
Jim Q

Subject: 🎁 Axios Finish Line: Taking the gift

Axios CEO Jim VandeHei is here with his weekly learnings on life and leadership. Share your thinking with him at jim@axios.com.

1 big thing: How to take feedback
Illustration of a newspaper with the Axios logo and a thought bubble.
Illustration: Lindsey Bailey/Axios

In the summer of 2004, hours before John Kerry's nomination speech at the Democratic convention, Washington Post political editor Maralee Schwartz gut-punched me with some brutal feedback.

I was covering Kerry for The Post. But she said I didn't write fast enough or think big enough to capture this historic moment. John Harris (a Post star who later co-founded Politico with us) got the call instead.
I was pissed. She was right.
Why it matters: "Feedback is a gift," the management gurus say. But in my experience running two companies, it's a gift most don't truly want.

It's true at work and in relationships. Every time my wife gives me feedback, I respond defensively, telling her all the reasons I rock. 😉
But learning to accept the gift with wisdom and humility is a superpower we all need. It's the gateway to growth.

Whether in a workplace or a relationship, feedback — honest, no-B.S. insight on what you could do better — is priceless. Too many people mess it up by talking instead of listening.
 Here's my blunt feedback about taking blunt feedback:

Listen! Don't make excuses or talk about the past. Actually, don't talk at all. Soak up, with self-confidence and humility, what the person is saying and take time before responding. When they're finished, you can say, "Good point" if you agree … or, "I hear you" if you want to think more about it. Or just: "Thank you."
Assume positive intent. The selfish approach for the other person would be to suppress what they really think. If someone has the guts to be frank with you, embrace it and thank them. When Mike asks for critiques from people, he says: "I promise to take it in the spirit it's intended."
Don't be defensive. That's the worst response to helpful feedback. It makes the person giving it feel unheard — and less likely to shoot straight with you in the future.
Ask for it. You're more likely to get feedback if you ask peers or superiors — in a sincere, humble, open-minded way — how you could be more effective. That projects strength, not weakness.
Act on it. If you show you're responsive, you'll get more input. And you'll get better at life and on the job.
Case in point: Often when you're giving a face-to-face review, people will validate and vindicate areas of weakness in the written eval.

"Jim doesn't listen" or "Jim makes too many excuses" or "Jim doesn’t welcome constructive criticism."
If I then start excuse-peddling or butt-covering, I've kind of made their point.  
The bottom line: Life is about forward motion. Elicit and take feedback to make your personal and professional performance tomorrow better than today.

