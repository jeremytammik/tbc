<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- Fair59 presents a very comprehensive and well explained solution to
  [Replicate Graphical Column Schedule Sort Order with C#]
  https://forums.autodesk.com/t5/revit-api-forum/replicate-graphical-column-schedule-sort-order-with-c/m-p/9105470
  by implementing a custom ColumnMarkComparer class implementing IComparer<FamilyInstance>
  I very strongly expect that you can implement something similar to solve your issue as well.
  [Sort Scheduled Elements base on it's Sort/Group Fields to be export in Excel]
  https://forums.autodesk.com/t5/revit-api-forum/sort-scheduled-elements-base-on-it-s-sort-group-fields-to-be/m-p/9142870

- 15913252 [Contradiction between shared dependencies used by Revit plugins]
  [Handling Third Party Library DLL Conflicts](https://thebuildingcoder.typepad.com/blog/2017/06/handling-third-party-library-dll-conflicts.html)

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### DLL Conflicts and Schedule Sort Order


####<a name="2"></a> I Finally Caved In

After several decades of hard resistance, I finally caved in.

Both my bank and my VPN network managers forced me to set up a smartphone.

I got an old Android S5, reset it to factory settings, installed the updates, and [removed all possible Samsung ballast](https://www.guidingtech.com/29921/remove-samsung-galaxy-s5).

It is working fine now, and I am still resisting all further temptation to go smartphone-crazy, not setting up dozens of apps or anything.
 
Just switching it on for the login credential checking, and then immediately off again.

Avoiding [smartphone danger](https://duckduckgo.com/?q=smartphone+danger);
[too much digital is bad for you](https://duckduckgo.com/?q=Too+much+digital+is+bad+for+you).


####<a name="3"></a> Handling Third Party Library DLL Conflicts

Back to a more technology friendly topic... or is it?

One recurring problematic topic is
on [handling third party library DLL conflicts](https://thebuildingcoder.typepad.com/blog/2017/06/handling-third-party-library-dll-conflicts.html).

A new query related to that came up, asking:

**Question:** I recently released a new add-in to my colleagues in the company.
Unfortunately, they report a failure starting it if they are already using another third-party add-in.
It throws the following file load exception:

<center>
<img src="img/fileloadexception_mvvmlight.png" alt="File load exception" width="553">
</center>

What can I do to fix this?

**Answer:** This looks like a conflict between two versions of a .NET assembly DLL that both of the add-ins are dependent on.

The two add-ins require different versions and a conflict ensues.

You are not the first to run into such an issue, as you can see from the list of old discussions 
on [handling third party library DLL conflicts](https://thebuildingcoder.typepad.com/blog/2017/06/handling-third-party-library-dll-conflicts.html) from 2017.

For new suggestions and solutions that came up since then, you can search
the [Revit API discussion forum](https://forums.autodesk.com/t5/revit-api-forum/bd-p/160) for
'dll conflict' and study more recent articles here on the blog:

- [Revit automatically initializes CefSharp](#https://thebuildingcoder.typepad.com/blog/2019/04/whats-new-in-the-revit-2020-api.html#4.1.1)
- [CefSharp DLL entanglement solution using IPC](#https://thebuildingcoder.typepad.com/blog/2019/04/set-floor-level-and-use-ipc-for-disentanglement.html#4)
- [External DLL loading](#https://thebuildingcoder.typepad.com/blog/2019/09/extensible-storage-external-event-dll-and-pipe-face.html#4)

As you can see from these examples, you have two choices:

- If Revit and / or several different add-ins use the same DLL, ensure that they all use the same version.
- If this is not possible for some reason, disconnect your usage of the problematic DLL from the Revit add-in `AppDomain` and use some other method to access the functionality you require, e.g., via [inter-process communication IPC](https://en.wikipedia.org/wiki/Inter-process_communication).

In this specific case, it sounds to me as if you are the creator of one add-in, and another add-in uses the same support DLL as you do.

In that case, I suggest you modify your add-in to work with whatever version of the support DLL happens to be loaded.

Alternatively, wrap the support DLL functionality into something that you have control over, so that you can do whatever you like with it.


####<a name="3"></a> Schedule Sort Order

**Question:**

**Answer:** 



####<a name="4"></a> 

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread


**Response:** 

<pre class="code">

</pre>



