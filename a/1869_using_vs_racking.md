<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- 17047221 [ADN Support]

- VS operation could not be Completed, Unspecified Error
  https://autodesk.slack.com/archives/C0SJ4U3PE/p1602528759049500
  Andrew Bushnell 
  A little PSA for revit developers.   I have noticed for a little bit now (and have gotten questions from others) that when building Revit or RevitAdditions within Visual Studio you get strange build failures and the message is along the lines of:
  21>Error: The operation could not be completed. Unspecified error
  Based on my investigations, this is an issue where the Visual Studio IDE gets itself into a strange state and the project in question has not been loaded properly for some reason.. I have not really traced down the steps that tend to cause, this.. For me I have found it related to switching project configurations before the build or even switching solutions from say revit to revitadditions.  It could also have to do with do a merge in git outside of the IDE that causes projects to change and reload.. Anyway, as I noted, I am not clear the exact steps, but when I do see the error, I found one of the following clears things up:
  - Close and reopen the solution in question.
  - Fully exit and restart Visual Studio.
  Anyway, FYI... (edited) 
  Jaap van der Weide  11 hours ago
  Could it have something to do with switching 3rd party header links when switching from debug to release?
  Andrew Bushnell  11 hours ago
  it may, but I have seen it just simple closing revit.sln and loading revitadditions, upon building tje newly loaded RevitAdditions.sln I would get the error..
  Dragos Turmac  2 hours ago
  Ah. That. Actually, I also got it in Precast,  which is a standalone C# solution with some C++ projects. It usually appears while trying to compile the C++ projects, which are a interop dependency for the C# projects - my theory is that somehow the C# binary locks the C++ one and doesn’t let msbuild query it for… god knows what, since interop is done at runtime afaik ( never seen this error on rebuild, only build ). And yes, only VS restart cures it; fortunately we rarely touch the C++ projects so I just ignored this error so far. Maybe we should look into it more and fill a MS bug if it’s so widespread? (edited) 
  Jeremy Tammik  < 1 minute ago
  I never touch C++ code, only C# Revit add-ins, and I see this message now and then as well. I just restart VS or even reboot Windows (under Parallels on Mac) and all is well.


twitter:

Keep things simple, or rack your brain. In one case, that can easily be avoided by using a using statement in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/usingusing

You should always keep things simple (I think).
The opposite can lead to racking your brain.
In one case at least, that can easily be avoided by using a <code>using</code> statement
&ndash; Avoid brain racking by using <code>using</code>
&ndash; On the VS operation unspecified error
&ndash; Native sons...

linkedin:

Keep things simple, or rack your brain. In one case, that can easily be avoided by using a using statement in the #RevitAPI

http://bit.ly/usingusing

You should always keep things simple (I think).
The opposite can lead to racking your brain.
In one case at least, that can easily be avoided by using a using statement:

- Avoid brain racking by using using
- On the VS operation unspecified error
- Native sons...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->


### Avoid Brain Racking by Using Using

You should always keep things simple (I think).
The opposite can lead to racking your brain.
In one case at least, that can easily be avoided by using a `using` statement:

- [Avoid brain racking by using `using`](#2)
- [On the VS operation unspecified error](#3)
- [Native sons](#4)

####<a name="2"></a> Avoid Brain Racking by Using Using

Here is a quick note on the importance
of [using `using` to automagically dispose and roll back](http://thebuildingcoder.typepad.com/blog/2012/04/using-using-automagically-disposes-and-rolls-back.html) transactions
and transaction groups, and the grief and brain racking that can easily ensue from failing to do so:

**Question:** For certain users, my add-in is hard crashing Revit to the desktop.

No exceptions, just everything closes.

I’ve got the journal files and I wanted to get some help seeing if there is anything to learn from them.

**Answer:** Sorry to hear about the hard crashes. And on certain users' systems, to boot.

Is the problem reproducible?

Does it only happen on certain specific machines?

Do you have any idea at all where the problem might be?

Does it send error reports to Autodesk?

What context can cause it?

Do you think it is due to your add-in, or Revit, or both?

**Response:** Thanks for the follow up.

I was able to get a hold of a customer’s computer that was seeing the error and track it down.

I had a `TransactionGroup` that was started but never completed &ndash; I did not call `Assimilate` or `Rollback`.

Adding the missing rollback fixed the error.

I did a review of all my other code and confirmed I closed all my other transaction groups and transactions after I opened them.

Relying on the destructor to call rollback obviously is a bad idea and was not intentional.

The relevant lines from the journal file look like this:

<pre>
' 7:< REGEN_DOC_CONTEXT_INFO: Changing wrong atom in regeneration
' 7:< REGEN_DOC_CONTEXT_INFO: Document is changed in regeneration while it is not supposed to
</pre>

The transaction wasn’t closed and Revit tried to regenerate.

**Answer:** Congratulations on solving the issue!

Thank you very much for your research and useful explanation.  

Were your transaction groups originally encapsulated in a `using` statement?

The Building Coder recommends doing so and claims that will obviate the need to call `Assimilate` or `Rollback`:
[Using Using Automagically Disposes and Rolls Back](http://thebuildingcoder.typepad.com/blog/2012/04/using-using-automagically-disposes-and-rolls-back.html).

I would be a rather shocked if that would turn out to be false.

So, I hope your transaction group causing the problem was not enclosed in a `using` statement.

If that is the case, I would strongly recommend ALWAYS enclosing transactions and transaction groups in `using` statements.

That would (I still hope) reliably avoid this problem forever.

Can't wait to hear your response on this...

**Response:** Brilliant!

No, I was not using `using` in my code.

Makes perfect sense to do so to prevent errors like mine.

I’m going to work on adding that to my code. There are 170 places I use a transaction, so it’s going to be a little work.

Your blog is a great resource. I use it quite regularly when trying to figure things out.

I don’t have any interesting code snippet to share.
It’s basically a `TransactionGroup` with an `Assimilate` call in an `IF` statement and a forgotten `Rollback` in the `ELSE`.

<center>
<img src="img/brain-rack.png" alt="Brain rack" title="Brain rack" width="300"/> <!-- 519 -->
</center>

####<a name="3"></a> On the VS Operation Unspecified Error 

A small note on the Visual Studio 'Operation could not be Completed, Unspecified Error':

You may have notices building Revit add-ins within Visual Studio getting strange build failures and a message along the lines of *21>Error: The operation could not be completed. Unspecified error*.

This seems to be an issue where the Visual Studio IDE gets itself into a strange state and the project in question has not been loaded properly for some reason.

I found it related to switching project configurations before the build or even switching solutions.

It could also have to do with do a merge in git outside of the IDE that causes projects to change and reload.

Anyway, without being clear the exact steps, when I do see the error, I found one of the following clears things up:

- Close and reopen the solution in question.
- Fully exit and restart Visual Studio.

Could it have something to do with switching header files, or switching from debug to release?

It may, but I have seen it just simple closing one solution and loading another.

I also got it in a standalone C# solution with some C++ projects.
It usually appears while trying to compile the C++ projects, which are a interop dependency for the C# projects &ndash; one theory is that somehow the C# binary locks the C++ one and doesn’t let msbuild query it for… god knows what, since interop is done at runtime afaik (never seen this error on rebuild, only build ).
And yes, only VS restart cures it; fortunately, we rarely touch the C++ projects, so I just ignored this error so far. 

Jeremy adds: I never touch C++ code, only C# Revit add-ins, and I see this message now and then as well.
I just restart VS or even reboot Windows (under Parallels on Mac) and all is well.


####<a name="4"></a> Native Sons

John Candela pays tribute to Native Americans for Monday,
[Indigenous People's Day](https://en.wikipedia.org/wiki/Indigenous_Peoples%27_Day), with
his track [Native Sons](https://soundcloud.com/jdcandela/native-sons):

<center>
<iframe width="500" height="100" scrolling="no" frameborder="no" allow="autoplay" src="https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/908029177&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true"></iframe><div style="font-size: 10px; color: #cccccc;line-break: anywhere;word-break: normal;overflow: hidden;white-space: nowrap;text-overflow: ellipsis; font-family: Interstate,Lucida Grande,Lucida Sans Unicode,Lucida Sans,Garuda,Verdana,Tahoma,sans-serif;font-weight: 100;"><a href="https://soundcloud.com/jdcandela" title="JC" target="_blank" style="color: #cccccc; text-decoration: none;">JC</a> · <a href="https://soundcloud.com/jdcandela/native-sons" title="Native Sons" target="_blank" style="color: #cccccc; text-decoration: none;">Native Sons</a></div>
</center>

