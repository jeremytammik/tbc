<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- 13580220 [Deletion of Global Parameters]

Deleting a Global Parameter and RevitPythonShell #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/delgloparpy

I have list of all global parameters from the active Revit document. I want to delete a specific global parameter from the list programmatically. Kindly suggest a way to delete a global parameter from the active document...

--->

### Deleting a Global Parameter and RevitPythonShell

This is another entry in the list of my attempts at teaching Revit API developers how to fish instead of feeding them.
Mostly, it ends up a mixture between the two, of course:

- [Creating a group and how to fish](http://thebuildingcoder.typepad.com/blog/2009/02/creating-a-group-and-how-to-fish.html)
- [Teaching a man how to fish and schedule creation](http://thebuildingcoder.typepad.com/blog/2014/07/teaching-a-man-how-to-fish-and-schedule-creation.html)
- [Trusted signature motivation and fishing](http://thebuildingcoder.typepad.com/blog/2016/09/trusted-signature-motivation-and-fishing.html)

This time, we address the question on how to:

- [Delete a global parameter](#2) and 
- [Test the result using RevitPythonShell](#3)

####<a name="2"></a>Deleting a Global Parameter

**Question:** I have list of all global parameters from the active Revit document. I want to delete a specific global parameter from the list programmatically. Kindly suggest a way to delete a global parameter from the active document.

**Answer:** Thank you for your query.

You can easily answer this question yourself, you know.

I did not know either, on first reading your question.

Here is the path I took to search for an answer:

I initially [searched the Internet for 'revit api delete global parameter'](https://duckduckgo.com/?q=revit+api+delete+global+parameter).

This led to some non-API discussions, such as the one
by [RevitCat](http://revitcat.blogspot.de) 
on [Deleting Global Parameters in Revit](http://revitcat.blogspot.de/2017/04/deleting-global-parameters-in-revit.html).

It also led to the official [developer guide discussion on managing global parameters](https://knowledge.autodesk.com/support/revit-products/getting-started/caas/CloudHelp/cloudhelp/2017/ENU/Revit-API/files/GUID-9FDC35A5-C054-46CA-B2DC-E20958FD197F-htm.html).

There, I learned that this is achieved programmatically using
the [`GlobalParametersManager` class](http://www.revitapidocs.com/2018.1/f3af05ec-1f0c-fe86-6708-0a211a40bcda.htm).

It does not provide any method to delete a global parameter.

However, the access to global parameters is provided by
the [`FindByName` method](http://www.revitapidocs.com/2018.1/7c7a7bd3-18e8-d9be-d9a7-66cd9ecdccc7.htm).

That method simply returns an element id.

This means that each global parameter is stored in the document database as a normal Revit element.

This means that it can be deleted using the `Document.Delete` method taking an `ElementId` or a collection, just like any other Revit element.

####<a name="3"></a>Testing Using RevitPythonShell

I decided to try this out on the fly
using [RevitPythonShell](https://github.com/architecture-building-systems/revitpythonshell).

I did not have it installed previously, but that can be achieved in seconds with a single click on
the [RevitPythonShell installer](https://github.com/architecture-building-systems/revitpythonshell#installation).

I then launched Revit and created a global parameter manually through the user interface:

<center>
<img src="img/global_parameter_01.png" alt="Global parameter" width="475"/>
</center>

Next, I started the interactive Python shell and ran the following code:

<pre class="prettyprint">
from Autodesk.Revit.DB import *
doc = __revit__.ActiveUIDocument.Document
id = GlobalParametersManager.FindByName(doc,'Test')
t = Transaction(doc)
t.Start('delete gp')
doc.Delete(id)
t.Commit()
</pre>

Immediately after running this code, the global parameter is no longer listed in the user interface.

I trust this solves your issue.

