<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


- Google's AlphaZero Destroys Stockfish In 100-Game Match
  https://www.chess.com/news/view/google-s-alphazero-destroys-stockfish-in-100-game-match
Chess changed forever today. And maybe the rest of the world did, too.
A little more than a year after AlphaGo sensationally won against the top Go player, the artificial-intelligence program AlphaZero has obliterated the highest-rated chess engine. 
Oh, and it took AlphaZero only four hours to "learn" chess...
AlphaZero was not "taught" the game in the traditional sense. That means no opening book, no endgame tables, and apparently no complicated algorithms dissecting minute differences between center pawns and side pawns. 
One expert said, "After reading the paper but especially seeing the games I thought, well, I always wondered how it would be if a superior species landed on earth and showed us how they play chess. I feel now I know."
Ex-champion Kasparov said, "We have always assumed that chess required too much empirical knowledge for a machine to play so well from scratch, with no human knowledge added at all... The ability of a machine to replicate and surpass centuries of human knowledge in complex closed systems is a world-changing tool."

- cad trends
  /a/doc/revit/tbc/git/a/zip/cad_trends_2019.pdf
  https://www.business-advantage.com/CAD-Trends-Webinars.php
  [Worldwide CAD Trends 2018/19 Survey Results](http://www.business-advantage.com/CAD-Trends-Webinar-2018.php)
  [Worldwide CAD Trends 2018/19 Survey Results](https://www.business-advantage.com/CAD-Trends-Results-2018.php)
  16 Key CAD trends were identified for the survey this year – most of the topics as researched in previous years, 4 topics removed and 6 new requested topics are added
  newly added to the list are marked with (*)
1. 3D Modelling
2. BIM (Building Information Modelling)
3. Cloud Based CAD
4. Mobile access to CAD (via laptops/tablets/mobile phones)
5. *Collaborative Design (team of people working on one design)
6. PLM (Product Lifecycle Management)
7. *Rendering (previously, advanced real-time
rendering)
8. CAD Licensing Options
9. PDM (Product Data Management)
10. Augmented Reality (use of non-geometrical data to augment a CAD model view with direct or indirect physical, real-world environment)
11. 2D Drafting
12. CAM
13. *Virtual Reality (computer-generated experience taking place within a simulated environment)
14. *Machine Learning (an application of artificial intelligence (AI) that provides the ability for systems to automatically learn and improve from experience without being explicitly programmed)
15. *Artificial Intelligence (the simulation of intelligent behaviour in computers)
16. *Generative Design (use of software to generate optimum forms for products and buildings based on design parameters)  
  Adoption Ratio, AWARENESS AND CURRENT USAGE
  Changes in Awareness Over Time, Usage over time
  IMPORTANCE RANKING
  Looking to the Future PREDICTED FUTURE USAGE
  
twitter:

Self-teaching AI, CAD trends and deprecated Revit API usage cleanup in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/deprecated2019

Same procedure as every year:
eliminate all deprecated Revit API usage warnings before even thinking of migrating to the next major release.
First, however, two other technical news items
&ndash; Self-teaching AI surpasses human knowledge
&ndash; CAD trends 2019
&ndash; Deprecated API usage warnings
&ndash; Replace <code>GetRules</code> by <code>GetElementFilter</code>
&ndash; Deprecated material asset accessors
&ndash; Update with zero compilation warnings...

linkedin:

Self-teaching AI, CAD trends and deprecated Revit API usage cleanup in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/deprecated2019

Same procedure as every year: eliminate all deprecated Revit API usage warnings before even thinking of migrating to the next major release.

First, however, two other technical news items:

- Self-teaching AI surpasses human knowledge
- CAD trends 2019
- Deprecated API usage warnings
- Replace <code>GetRules</code> by <code>GetElementFilter</code>
- Deprecated material asset accessors
- Update with zero compilation warnings...

of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.145.4).
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) recently

-->

### AI, Trends and Yearly API Usage Cleanup

Same procedure as every year:
eliminate all deprecated Revit API usage warnings before even thinking of migrating to the next major release.

First, however, two other technical news items:

- [Self-teaching AI surpasses human knowledge](#2) 
- [CAD trends 2019](#3) 
- [Deprecated API usage warnings](#4) 
- [Replace `GetRules` by `GetElementFilter`](#5) 
- [Deprecated material asset accessors](#6) 
- [Update with zero compilation warnings](#7) 


<center>
<img src="img/alphazero_vs_stockfish_8.png" alt="AlphaZero vs. Stockfish 8" width="363">
</center>

#### <a name="2"></a> Self-Teaching AI Surpasses Human Knowledge

I followed the progress of [AlphaGo](https://en.wikipedia.org/wiki/AlphaGo) with great fascination.

Now, [AlphaZero](https://en.wikipedia.org/wiki/AlphaZero) seems more fascinating still:

> AlphaZero is a computer program or algorithm by DeepMind to master go, chess and shogi. On December 5, 2017 the DeepMind team released a preprint introducing AlphaZero, which, within 24 hours, achieved a superhuman level of play in these three games by defeating world-champion programs, Stockfish, elmo, and the 3-day version of AlphaGo Zero, in each case making use of custom tensor processing units (TPUs) that the Google programs were optimized to use.
AlphaZero was trained solely via "self-play" using 5,000 first-generation TPUs to generate the games and 64 second-generation TPUs to train the neural networks, all in parallel, with no access to opening books or endgame tables.
After ... 9 hours of training, the algorithm decisively defeated Stockfish 8 in a time-controlled 100-game tournament (28 wins, 0 losses, and 72 draws)...

[Chess.com](https://www.chess.com) describes the chess implications in the article [Google's AlphaZero Destroys Stockfish In 100-Game Match](https://www.chess.com/news/view/google-s-alphazero-destroys-stockfish-in-100-game-match):

> Chess changed forever today. And maybe the rest of the world did, too.

> A little more than a year after AlphaGo sensationally won against the top Go player, the artificial-intelligence program AlphaZero has obliterated the highest-rated chess engine. 

> Oh, and it took AlphaZero only four hours to "learn" chess...

> AlphaZero was not "taught" the game in the traditional sense. That means no opening book, no endgame tables, and apparently no complicated algorithms dissecting minute differences between center pawns and side pawns. 

> One expert said, "After reading the paper but especially seeing the games I thought, well, I always wondered how it would be if a superior species landed on earth and showed us how they play chess. I feel now I know."

> Ex-champion Kasparov said, "We have always assumed that chess required too much empirical knowledge for a machine to play so well from scratch, with no human knowledge added at all... The ability of a machine to replicate and surpass centuries of human knowledge in complex closed systems is a world-changing tool."


#### <a name="3"></a> CAD Trends 2019

Possibly more relevant to us today, or possibly not,
the [Business Advantage Group](https://www.business-advantage.com) published
its survey of 2018/2019 CAD trends in the form of both
a [webinar recording](http://www.business-advantage.com/CAD-Trends-Webinar-2018.php)
and [survey results](https://www.business-advantage.com/CAD-Trends-Results-2018.php).

16 Key CAD trends were identified for the survey this year; newly added ones italic:

- 3D Modelling, BIM, Cloud Based CAD, Mobile access to CAD, *Collaborative Design*, PLM, *Rendering*, CAD Licensing Options, PDM, Augmented Reality, 2D Drafting, CAM, *Virtual Reality*, *Machine Learning*, *Artificial Intelligence*, *Generative Design*

The analysis includes aspects such as:

- Adoption ratio, Awareness, Current usage, Changes in awareness over time, Usage over time, Importance ranking, Looking to the future and Predicted future usage.

Download the webinar or survey results from the links above in case you are interested in the detailed results.


#### <a name="4"></a> Deprecated API Usage Warnings

Back to the nitty-gritty details of the Revit API...

As pointed out above, we expect a new major release sometime in the future, and I highly recommend eliminating all deprecated Revit API usage warnings before even thinking of migrating to the next major release.

Before I started out eliminating the deprecated Revit API usage warnings,
compiling [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
generated [eight warnings](zip/tbc_samples_2019_errors_warnings_2.txt).

The first is just a notification of unreachable code in the module `CmdListAllRooms.cs` that I recently worked on
to [export room boundaries to CSV for Forge surface classification](https://thebuildingcoder.typepad.com/blog/2019/01/room-boundaries-to-csv-and-wpf-template.html#2),
so I'll fix that first.

It was caused by a `const bool` variable `_exportInMillimetres`.
The warning disappeared after I changed that to `static bool` instead.

That leaves [seven warnings](zip/tbc_samples_2019_errors_warnings_3.txt), all numbered `CS0618` and caused by deprecated Revit API usage, from two different API calls:

- In CmdCollectorPerformance.cs: `ParameterFilterElement.GetRules` is obsolete: This method is deprecated in Revit 2019 and will be removed in the next version of Revit. We suggest you use `GetElementFilter` instead.
- In CmdGetMaterials.cs, repeated six times: `AssetProperties.this[]` is obsolete: This property is deprecated in Revit 2019 and will be removed in the next version of Revit. We suggest you use the `FindByName(String)` or `Get(int)` method instead.

Both of these deprecated API usages can be easily fixed by following the instructions given by the warning messages.


#### <a name="5"></a> Replace GetRules by GetElementFilter

To eliminate the first warning, we can replace `GetRules` by `GetElementFilter` and simplify the code using it like as follows; before:

<pre class="code">
  <span style="color:#2b91af;">ParameterFilterElement</span>&nbsp;pfe ...

&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">FilterRule</span>&nbsp;rule&nbsp;<span style="color:blue;">in</span>&nbsp;pfe.GetRules()&nbsp;)&nbsp;<span style="color:green;">//&nbsp;2018</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;elemsByFilter2
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;elemsByFilter.Where(&nbsp;e
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;rule.ElementPasses(&nbsp;e&nbsp;)&nbsp;);
&nbsp;&nbsp;}
</pre>

After:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">ElementFilter</span>&nbsp;ef&nbsp;=&nbsp;pfe.GetElementFilter();&nbsp;<span style="color:green;">//&nbsp;2019</span>
&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;elemsByFilter2
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;elemsByFilter.WherePasses(&nbsp;ef&nbsp;);
</pre>

#### <a name="6"></a> Deprecated Material Asset Accessors

That leaves [six warnings caused by calls to deprecated material asset accessors](zip/tbc_samples_2019_errors_warnings_4.txt).

Again, easily eliminated by doing what the man says, calling `Get` for `int` accessors and `FindByName` for the ones taking a string argument.


#### <a name="7"></a> Update with Zero Compilation Warnings

The cleaned-up code is available
from [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2019.0.145.11](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.145.11).

We are now ready to face whatever it takes to migrate to the new major release, if and when that shows up.

