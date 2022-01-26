<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- open source
  https://github.com/chuongmep/RevitAddInManager
  /Users/jta/a/doc/revit/tbc/git/a/img/RevitAddInManager.png

- [FormulaManager Class](https://www.revitapidocs.com/2022/d061dadf-70da-a883-ec12-5cf98ded069e.htm)
  [Create user extensible functionality](https://forums.autodesk.com/t5/revit-api-forum/create-user-extesible-funcionality/m-p/10887473)

twitter:

Debug and load your add-ins with the new open source Add-In Manager; add formulas and scripting support to your own apps with the #RevitAPI FormulaManager @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://autode.sk/osaddinmanager

Exciting news around debugging and loading add-ins and adding formulas and scripting support to your own apps
&ndash; Open source Add-In Manager
&ndash; FormulaManager and scripting support
&ndash; Happy New Year of the Tiger 虎...

linkedin:

Debug and load your add-ins with the new open source Add-In Manager; add formulas and scripting support to your own apps with the #RevitAPI FormulaManager

https://autode.sk/osaddinmanager

- Open source Add-In Manager
- FormulaManager and scripting support
- Happy New Year of the Tiger 虎...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Add-In Manager, FormulaManager and Tiger Year

Exciting news around debugging and loading add-ins and adding formulas and scripting support to your own apps:

- [Open source Add-In Manager](#2)
- [FormulaManager and scripting support](#4)
- [Happy New Year of the Tiger 虎](#5)

####<a name="2"></a> Open Source Add-In Manager

Add-in developers have been clamouring for ages for the Revit development team
to [open source the Add-In Manager](https://forums.autodesk.com/t5/revit-ideas/open-source-add-in-manager/idi-p/8049456);
the corresponding Revit Idea Station wish list item was raised in 2018 and has gathered 49 votes, and the request was originally raised and discussed earlier still.

Chuong Ho now took action and asks for your support:

> Hi, all Developers working with Revit API, it's time;
we need to improve addin manager tool for a long time.
Now I'm in the process of developing and maintaining it on open source basis with more features to support developers easier access to Revit API.
All the programmers in the world can help to make this product better for developer.
Currently developing in my free time so nothing is perfect right now.
Link to project open source at:

> <p style="text-align:center"><a href="https://github.com/chuongmep/RevitAddInManager">github.com/chuongmep/RevitAddInManager</a></p>

<center>
<img src="img/RevitAddInManager.png" alt="RevitAddInManager" title="RevitAddInManager" width="386"/> <!-- 386 -->
</center>

Many thanks to Chuong Ho for this great initiative!

Comments on LinkedIn:

- It's great to see advancements on the development of this tool, thank you!
- Yes, only make the tool support developer better, anyway we still need a tool that programmers all over the world can modify and ask for ideas.
- Add-in manager became less useful with the hot-reload feature of the latest release of Visual Studio,
[apply code changes](https://thebuildingcoder.typepad.com/blog/2021/10/localised-forge-intros-and-apply-code-changes.html#4).
I had some ideas on improving it a while ago, but when the project got bigger it appeared more reasonable an actually not-that-hard to use standard way to debug Revit plugins.

By the way, I have started to use the <i>apply code changes</i> method now as well.
It works fine and I love it.
Reminds me of the good all days
with [edit and continue](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.49),
which worked just a smoothly back then.

####<a name="4"></a> FormulaManager and Scripting Support 

Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
points out the existence and shows how to use
the [FormulaManager class](https://www.revitapidocs.com/2022/d061dadf-70da-a883-ec12-5cf98ded069e.htm) in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [creating user extensible functionality](https://forums.autodesk.com/t5/revit-api-forum/create-user-extesible-funcionality/m-p/10887473):

**Question:** I am creating a program that allows me to quantify elements. 
I calculate column surface areas using different formulas for interior and exterior columns.
During the modelling process, the user may want to create new definitions, e.g., for a central column, with its own formula `get_area`.
How could I implement support for the end user to add such functionality?

**Answer:** Several useful suggestions were made using the powerful built-in .NET scripting functionality.
Richard adds a pure Revit solution, saying:

You may find `FormulaManager.Evaluate` offers a more Revit centric approach.
However, it seems to imply that a parameter element is required:

> It evaluates formula using list of global or family parameters depends on document type.

This probably means you have to be in a family document to evaluate a family parameter and a project to evaluate a global one.
I guess you could make it work via adding what you need in a temporary way if it is requiring a parameter of some kind, i.e., a global one in project (although you wouldn't be able to reference other parameter names in the formula string).
Here is a simple example that works:

<pre class="code">
  Public Function Obj_220118a(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result
    Dim app = commandData.Application
    Dim uidoc = commandData.Application.ActiveUIDocument
    Dim IntDoc = uidoc.Document
    Dim Formula As String = "(10*10)^0.5"
    Dim Formula0 As String = "Pi()"
    Dim Out As String = ""
    Using Tx As New Transaction(IntDoc, "XX")
      If Tx.Start Then
        Dim G As String = Guid.NewGuid.ToString
        Dim GP = GlobalParameter.Create(IntDoc, "RPT_" & G, SpecTypeId.Number)
        Out = FormulaManager.Evaluate(GP.Id, IntDoc, Formula0)
        Tx.RollBack()
      End If
    End Using
    TaskDialog.Show("Result", Out)
    Return Result.Succeeded
  End Function
</pre>

Many thanks to Richard for pointing out and sharing this!

####<a name="5"></a> Happy New Year of the Tiger 虎

Before leaving, I wish you all a 
Happy [Chinese New Year](https://en.wikipedia.org/wiki/Chinese_New_Year),
the [Year of the Tiger](https://en.wikipedia.org/wiki/Tiger_(zodiac)),
beginning next Tuesday, February 1.

<center>
<img src="img/2022-01-26_tiger_year.jpg" alt="Year of the Tiger" title="Year of the Tiger" width="440"/> <!-- 1100 -->
</center>
