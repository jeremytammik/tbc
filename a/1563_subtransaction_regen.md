<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- AI News
  https://www.theverge.com/2017/5/27/15704088/alphago-ke-jie-game-3-result-retires-future
  [Startup uses AI to create programs from simple screenshots](https://siliconangle.com/blog/2017/05/28/startup-uses-ai-create-gui-source-code-simple-screenshots)
  research paper [pix2code: Generating Code from a Graphical User Interface Screenshot](https://arxiv.org/pdf/1705.07962.pdf)
  
- Александр Игнатович Re: for the tbc blogpost

AI beats all + invents new moves post 3000 year @AutodeskForge #ForgeDevCon #RevitAPI @AutodeskRevit #bim #dynamobim http://bit.ly/ai_sub_tx_regen

Sub-Transaction requires regeneration @AutodeskForge #ForgeDevCon #RevitAPI @AutodeskRevit #bim #dynamobim http://bit.ly/ai_sub_tx_regen

Things continue moving fast in AI, and the need to regenerate in the Revit API remains unchanged
&ndash; AI News
&ndash; Subtransaction Requires Regeneration...

-->

### AI News and Sub-Transaction Regen

Things continue moving fast in AI, and
the [need to regenerate](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.33) in
the Revit API remains unchanged:

- [AI News](#2)
- [Sub-Transaction Requires Regeneration](#3)


#### <a name="2"></a>AI News

I [reported](http://thebuildingcoder.typepad.com/blog/2016/01/bim-programming-madrid-and-spanish-connectivity.html#7) on
the surprising success of [AlphaGo](https://en.wikipedia.org/wiki/AlphaGo) 18
months ago, when it unexpectedly defeated legendary player Lee Se-dol 4-1, cf.
the [DeepMind Go challenge overview](https://www.theverge.com/google-deepmind).

That revolutionised the expectation and perception of AI of scientists and the industry alike: AI is indeed capable of extracting and generating knowledge that exceeds the best human capacity.

Now another important step was taken, completely settling the matter:
[AlphaGo retires from competitive Go after defeating world number one Ke Jie 3-0](https://www.theverge.com/2017/5/27/15704088/alphago-ke-jie-game-3-result-retires-future).

In this game, Ke Jie made use of some unconventional new moves that AlphaGo invented and first demonstrated in its previous public games... nota bene, it invented new moves after 3000 years of human exploration... one question this raises: 'Invent'? Or discover?

<center>
<img src="img/go_game_kobayashi_kato.png" alt="Go game" width="300"> <!-- 437 -->
</center>

The DeepMind research team behind AlphaGo has conclusively proved its point and is moving on to new and greener pastures.

Another recent impressive example of what AI enables is provided by
the [startup that uses AI to create programs from simple screenshots](https://siliconangle.com/blog/2017/05/28/startup-uses-ai-create-gui-source-code-simple-screenshots),
cf. the corresponding [research paper *pix2code: Generating Code from a Graphical User Interface Screenshot*](https://arxiv.org/pdf/1705.07962.pdf).

Exciting times indeed.

Back to the Revit API:


#### <a name="3"></a>Sub-Transaction Requires Regeneration

Alexander Ignatovich, [@CADBIMDeveloper](https://github.com/CADBIMDeveloper),
aka Александр Игнатович, shares another important example that fits in perfectly with our continuing series demonstrating
the [need to regenerate](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.33).

In his own words:

> It's time to share some piece of knowledge about sub-transactions in your brilliant blog ;-)

> I've spent some time, so it should be documented somewhere.

> As most Revit programmers know, the document automatically regenerates on transaction commit.

> My problem was, that sub-transactions do not cause this, as I previously assumed.

> I wanted to retrieve family symbol geometry. I activated the family symbol earlier in a sub-transaction, but I didn't call the `document.Regenerate` method, so the `familySymbol.get_Geometry` method returned null. I was confused, because in the debugger I saw that my family symbol is active. I also looked at the activated family symbol geometry, and it was not null.

> After I added document regeneration, my code execution path looks like this:

<pre class="code">
  start main transaction
  {
    ...
    
    start sub transaction
    {
      ...
      
      if (!familySymbol.IsActive)
        familySymbol.Activate()
      
      ...
      
      subtransaction.Commit()
    }
    
    document.Regenerate()
    
    ...
    
    geometry = familySymbol.get_Geometry(options)
    
    ...
  }
</pre>

> I previously considered sub-transactions as mini transactions with practically the same behaviour, except they are nested to the transaction and make real changes to model only when outer transaction is committed.

> Now I understand that nothing is really committed automatically to the Revit database before completion and commitment of the outer-most transaction.

Many thanks to Alexander for explicitly pointing this out!

