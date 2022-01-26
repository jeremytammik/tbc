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

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

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

### 



####<a name="2"></a> 

**Question:** 


**Answer:** 

####<a name="3"></a> 


<center>
<img src="img/.png" alt="" title="" width="223"/> <!-- 223 -->
</center>



####<a name="4"></a> 


Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas


<pre class="code">


</pre>



Add-in developers have benn clamluring for for ages for the Revit development team
to [open source the Add-In Manager](https://forums.autodesk.com/t5/revit-ideas/open-source-add-in-manager/idi-p/8049456);
the corresponding Revit Idea Station wish list item was raised in 2018 and has gathered 49 votes, and the request was originally raised and discussed earlier still.




open source
https://github.com/chuongmep/RevitAddInManager
/Users/jta/a/doc/revit/tbc/git/a/img/RevitAddInManager.png

Chuong Ho
Hi all Developer working with Revit API,
it's time we need to improve addin manager tool for a long time, now I'm in the process of developing and maintaining them on open source basis with more features to support developers easier access to revit api.All the programmers in the world can help to make this product better for developer.
Currently developing in my free time so nothing is perfect right now
Link project open source can search at 
https://github.com/chuongmep/RevitAddInManager
Comments:
It's great to see advancements on the development of this tool, thanks you!
Add-in manager became less useful with hot-reload feature of the latest release of Visual Sudio. I had some ideas on improving it a while ago, but when the project got bigger it appeared more reasonable an actually not-that-hard to use standard way to debug Revit plugins.
Yes, only make the tool support developer better , anyway we still need a tool that programmers all over the world can modify and ask for ideas.



[FormulaManager Class](https://www.revitapidocs.com/2022/d061dadf-70da-a883-ec12-5cf98ded069e.htm)
rpthomas
[Create user extensible functionality](https://forums.autodesk.com/t5/revit-api-forum/create-user-extesible-funcionality/m-p/10887473)
[Q] I am creating a program that allows me to quantify elements. 
I calculate column surface areas using different formulas for interior and exterior columns.
During the modelling process, the user may want to create new definitions, e.g. for a central column, with its own formula `get_area`.
How could I implement support for the user to add such functionality?
[A] Several useful suggestions were made using the powerful .NET functionality.
RPT adds a pure Revit solution, saying:
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

Many thanks to Richard for sharing this!


####<a name="5"></a> Happy New Year of the Tiger 虎

Happy [Chinese New Year](https://en.wikipedia.org/wiki/Chinese_New_Year),
the [Year of the Tiger](https://en.wikipedia.org/wiki/Tiger_(zodiac))!

<center>
<img src="img/2022-01-26_tiger_year.jpg" alt="Year of the Tiger" title="Year of the Tiger" width="440"/> <!-- 1100 -->
</center>
