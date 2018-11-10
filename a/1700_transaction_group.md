<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-api-forum/combine-multiple-transactions-into-one-undo/m-p/8393152
  Mentioned in the discussion on [Using Transaction Groups](https://thebuildingcoder.typepad.com/blog/2015/02/using-transaction-groups.html)

More on transaction groups and assimilation in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/txgroups

We return to
the question on how
to combine multiple transactions into one undo with a new answer by Arnošt Löbel, ex Senior Principal Engineer of the Revit development team
&ndash; How do I know how many transactions, and which ones, are assimilated into group?
&ndash; What if I start a transaction before starting a group (in the same API context) and then call <code>Assimilate</code>, will this ahead started one also be assimilated into the group?
&ndash; I can leave the API context (external event or <code>Execute</code> method) with the transaction group still open...

-->

### More on Transaction Groups and Assimilation

This is The Building Coder post number 1700.

It is a great pleasure to return to
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) question on how
to [combine multiple transactions into one undo](https://forums.autodesk.com/t5/revit-api-forum/combine-multiple-transactions-into-one-undo/m-p/8393152) and
highlight a comeback and new answer by Arnošt Löbel, ex Senior Principal Engineer of the Revit development team.

The beginning of that thread and Arnošt's previous answers were already presented here in the blog in the discussion
on [using transaction groups](https://thebuildingcoder.typepad.com/blog/2015/02/using-transaction-groups.html).

**Question:** Hi Arnost Lobel:

I have read your reply, and looked into the `TransactionGroup.Assimilate` method in the Revit API help document `.chm` file.

I found that this method takes no parameters.

What  I want to ask is:

1. How do I know how many transactions, and which ones, are assimilated into group?
2. What if I start a transaction before starting a group (in the same API context) and then call `Assimilate`, will this ahead started one also be assimilated into the group?
3. Another interesting thing that I found is that I can leave the API context (external event or `Execute` method) with the transaction group still open. See my code:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Execute(&nbsp;RvtUiApplication&nbsp;app&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;WpfTarget.Transactions.ContainsKey(&nbsp;<span style="color:blue;">this</span>.GetName()&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Transaction&nbsp;=&nbsp;WpfTarget.Transactions[GetName()];
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Transaction&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WpfTarget.CmdVars.DbDoc,&nbsp;GetName()&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WpfTarget.Transactions[GetName()]&nbsp;=&nbsp;<span style="color:blue;">this</span>.Transaction;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WpfTarget.Transactions.Add(&nbsp;GetName(),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WpfTarget.CmdVars.DbDoc,&nbsp;GetName()&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;WpfTarget.TransGroup.Start();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Transaction</span>.Start();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Level</span>.Create(&nbsp;WpfTarget.CmdVars.DbDoc,&nbsp;30&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Transaction</span>.Commit();
 
&nbsp;&nbsp;&nbsp;&nbsp;WpfTarget.TransGroup.Assimilate();
&nbsp;&nbsp;}
</pre>

Just ignore the head part of this `Execute` method, they are for my own WPF window. Notice that before leaving this external event handler context, I called only `Assimilated`, but not `Rollback` or `Commit`.

I ran these codes without debugging, and Revit threw no errors.

Tested in API ver 2018.

If running in VS debugger, single stepping out of this method displays a page like this:

<center>
<img src="img/apimfc_pdb_not_loaded.png" alt="APIMFC.pdb not loaded" width="601">
</center>

Does it mean that I can 'modelessly' use `TransactionGroup`?

**Answer:** Now this is rather funny, I think. I have found this post accidentally while googling up something else which had my name in it. I was surprised to see a new question to a very old post I had participated in, and even more I was surprised to find the question unanswered. I do not work for Autodesk anymore, but since the transaction API was kind of a favourite of mine when I worked on the API, I kind of feel somehow obligated to answer such questions. I mean &ndash; I would not look for them purposely, of course, but if I come across one, I’ll answer it if I can.

To your questions:

1. You would not know how many transactions would be assimilated in a transaction group unless you keep a count of the committed transactions by yourself (though I do not know why you would need to do that). A transaction groups simply assimilates (when assimilated) all transactions that started and were committed inside the group.
2. The previous answer sort of also answers your second question &ndash; you cannot start a transaction group while there is a transaction still open. The `TransactionGroup.Start` method would throw an exception in such a situation. To test whether there is a transaction open, you can use the `Document.IsModifiable` method.
3. To your third question &ndash; it should not be possible to leave your command context with either a transaction or transaction group still open. Well, technically you can do that, of course, but if you do, Revit will roll them back for you and you would lose all changes made in your external command. The internal implementation might have changed in the last three years, I suppose, but it used to be the following way: Before an external command was launched, Revit would start an internal transaction group. Then the command was executed and upon returning from it, be that a natural exit or an exception, Revit would test whether there were any transactions or transaction groups open. If there are, Revit would force them to be rolled back and then it would follow with rolling back the internal transaction group which started before the external command was invoked. This is all for safety reasons; Revit is cautious about external commands that forgot to close all their transactions and transaction groups.

I hope it makes sense what I wrote.

I also have one comment about your code. One thing that is not quite clear to me is what happens if your container of transactions does not have an entry for `this.GetName`; The code would proceed to the `Else` block in which a new transaction is instantiated and added to your container of transactions. However, the member `this.Transaction` is not updated, which seems strange with respect to the rest of your code. After you leave your if-else block and you start your transaction group, you then call `Transaction.start` &ndash; which transaction would that be? Do you have a global `Transaction` instance? I assume you do, because in this particular case your `this.Transaction` would not be set to the new transaction you instantiated inside the `Else` block. My guess is that you would start and commit a different transaction (if there was one instantiated before), which would have a different name. Of course, you would not notice that if you successfully assimilated the transaction group, which is probably why that detail escaped you.

Very many thanks to Arnošt for jumping in and helping out again!

I hope you are having a great time and enjoying life post-Revit!
