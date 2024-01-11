<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- <script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script> -->
<!-- https://highlightjs.org/#usage -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
</head>

<!---

- logging:
  21768403 [Assistance Required with Revit Journal Files]
  https://forums.autodesk.com/t5/revit-api-forum/api-access-to-user-history-quot-show-history-quot/m-p/12472116
  AdWindows -- https://forums.autodesk.com/t5/revit-api-forum/intercepting-commands-executed-by-keyboard-shortcuts/td-p/12457597
  https://forums.autodesk.com/t5/revit-api-forum/the-syntax-and-structure-of-the-journal-file-in-revit/m-p/12490089

- aec greenwashing -- https://www.linkedin.com/pulse/can-construction-refrain-from-greenwashing-aarni-heiskanen-dqfuf/?midToken=AQFbSE8RCQ2a5g&midSig=3AqoWZGBDAwH41&trk=eml-email_series_follow_newsletter_01-newsletter_hero_banner-0-open_on_linkedin_cta&trkEmail=eml-email_series_follow_newsletter_01-newsletter_hero_banner-0-open_on_linkedin_cta-null-7371t~lr3k4lr6~ie-null-null&eid=7371t-lr3k4lr6-ie

- https://climpact.ch
  developed at [EPFL](https://www.epfl.ch), the École Polytechnique Fédérale de Lausanne,
  > Our actions have an impact on the climate.
  Is our perception accurate?
  Take our quiz to develop an intuition about how to reduce your carbon footprint, and to help us understand how people perceive the impact of their actions.

- interesting AI motivation: ensure it goes well:
  Tristan Hume https://thume.ca/
  All my favorite tracing tools: eBPF, QEMU, Perfetto, new ones I built and more
  https://thume.ca/2023/12/02/tracing-methods/
  > AI now is still missing a lot, but progress is incredibly fast. It’s hard for me to say the coming decade of progress won’t lead to AI as good as us at nearly all jobs, which would be the biggest event in history. Anthropic is unusually full of people who joined because they really care about ensuring this goes well. I think we have the world’s best alignment, interpretability research, and AI policy teams, and I personally work on performance optimization here because I think it’s the best way to leverage my comparative advantage to help the rest of our efforts succeed at steering towards AI going well for the world in the event it keeps up this pace.

- sublime text
  https://thume.ca/2017/03/04/my-text-editor-journey-vim-spacemacs-atom-and-sublime-text/

- Can anyone share a working implementation of WPF progress bar with abort button?
  https://forums.autodesk.com/t5/revit-api-forum/can-anyone-share-a-working-implementation-of-wpf-progress-bar/m-p/12427800#M75752
  already blogged about:
  https://github.com/SpeedCAD/SCADtools.Revit.UI.ProgressMeter

- BIM-GPT: a Prompt-Based Virtual Assistant Framework for BIM Information Retrieval
  https://arxiv.org/abs/2304.09333
  > Efficient information retrieval (IR) from building information models (BIMs) poses significant challenges due to the necessity for deep BIM knowledge or extensive engineering efforts for automation. We introduce BIM-GPT, a prompt-based virtual assistant (VA) framework integrating BIM and generative pre-trained transformer (GPT) technologies to support NL-based IR. A prompt manager and dynamic template generate prompts for GPT models, enabling interpretation of NL queries, summarization of retrieved information, and answering BIM-related questions. In tests on a BIM IR dataset, our approach achieved 83.5% and 99.5% accuracy rates for classifying NL queries with no data and 2% data incorporated in prompts, respectively. Additionally, we validated the functionality of BIM-GPT through a VA prototype for a hospital building. This research contributes to the development of effective and versatile VAs for BIM IR in the construction industry, significantly enhancing BIM accessibility and reducing engineering efforts and training data requirements for processing NL queries.
  bim_gpt_prompt_manager_nl_processing.png

- Revit ID Compilation
  Revit IDs
  https://forums.autodesk.com/t5/revit-api-forum/revit-ids/td-p/12418195

twitter:

 #RevitAPI @AutodeskRevit @AutodeskAPS #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Logging


####<a name="2"></a> logging:

logging:
21768403 [Assistance Required with Revit Journal Files]
https://forums.autodesk.com/t5/revit-api-forum/api-access-to-user-history-quot-show-history-quot/m-p/12472116
AdWindows -- https://forums.autodesk.com/t5/revit-api-forum/intercepting-commands-executed-by-keyboard-shortcuts/td-p/12457597
https://forums.autodesk.com/t5/revit-api-forum/the-syntax-and-structure-of-the-journal-file-in-revit/m-p/12490089

####<a name="2"></a> Can anyone share a working implementation of WPF progress bar with abort button?

Can anyone share a working implementation of WPF progress bar with abort button?
https://forums.autodesk.com/t5/revit-api-forum/can-anyone-share-a-working-implementation-of-wpf-progress-bar/m-p/12427800#M75752
already blogged about:
https://github.com/SpeedCAD/SCADtools.Revit.UI.ProgressMeter

####<a name="2"></a> Revit ID Compilation

Revit ID Compilation
Revit IDs
https://forums.autodesk.com/t5/revit-api-forum/revit-ids/td-p/12418195
>>>>Element Id
0344:Newly Created Element Retrieval Based on Monotonously Increasing Element Id Values
0344:Enhanced Parameter Filter for Greater Element Id Values
0544:Comparing Element Id for Equality
0948:Element Ids in Extensible Storage
1144:Element Id &ndash; Export, Unique, Navisworks and Other Ids
1144:Negative Element Ids and Element Property Drop-down List Enumerations <!-- dropdown combo -->
1182:How to Trigger a Dynamic Model Updater by Specific Element Ids
1353:Family Category, Element Ids, Transaction and Updates
1396:<"#6">WPF Element Id Converter
1577:<"#3">Access Revit BIM Data and Element Ids from BIM360
1628:Retrieving Newly Created Element Ids
1628:<"#3">Consecutive Element Ids
1634:<"#2">Search and Snoop by Element Id or Unique Id
1762:Element Identifiers in RVT, IFC, NW and Forge
1762:<"#3"> Revit Element Ids in Forge via Navisworks and IFC
1959:<"#2"> Immutable UniqueId, Mutable Element Id
1974:64-Bit Element Ids, Maybe?
1974:<"#2"> 64-Bit Element Ids
1992:<"#9"> Consuming Huge Numbers of Element Ids
1995:<"#4"> Backward Compatible 64 Bit Element Id
>>>>0344 0544 0948 1144 1182 1353 1396 1577 1628 1634 1762 1959 1974 1992 1995
<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/04/retrieving-newly-created-elements-in-revit-2011.html">Retrieving Newly Created Elements in Revit 2011</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/02/comparing-element-id-for-equality.html">Comparing Element Id for Equality</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/05/dwg-issues-and-various-other-updates.html">DWG Issues and Various Other Updates</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/04/element-id-export-unique-navisworks-and-other-ids.html">Element Id &ndash; Export, Unique, Navisworks and Other Ids</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/07/createlinkreference-sample-code.html">CreateLinkReference Sample Code</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/09/family-category-element-ids-transaction-undo-and-updates.html">Family Category, Element Ids, Transaction Undo and Updates</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/01/devday-conference-in-munich-and-wpf-doevents.html">DevDay Conference in Munich and WPF DoEvents</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/08/revit-versus-forge-ids-and-add-in-installation.html">Revit versus Forge, Ids and Add-In Installation</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2018/02/retrieving-newly-created-element-ids.html">Retrieving Newly Created Element Ids</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2018/03/revitlookup-search-and-snoop-by-element-and-unique-id.html">RevitLookup Search by Element and Unique Id</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/07/element-identifiers-in-rvt-ifc-nw-and-forge.html">Element Identifiers in RVT, IFC, NW and Forge</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2022/07/immutable-uniqueid-and-revit-database-explorer.html">Immutable UniqueId and Revit Database Explorer</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2022/11/64-bit-element-ids-maybe.html">64-Bit Element Ids, Maybe?</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2023/04/configuring-rvtsamples-2024.html">Configuring RvtSamples 2024 and Big Numbers</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2023/05/64-bit-ids-revit-and-revitlookup-updates.html">64 Bit Ids, Revit and RevitLookup Updates</a></li>
</ul>
>>>>Unique Id
0104:UniqueId versus DWF and IFC GUID
0104:GUID and UniqueId
0104:UniqueId to GUID Encoding
0104:IFC GUID and UniqueId Encoder and Decoder
0737:Retrieving Unique Geometry Vertices
0787:Geometry Traversal to Retrieve Unique Vertices
0819:IFC GUID Generation and Uniqueness
0943:Solving the Non-unique Unique Id Problem
1144:Element Id &ndash; Export, Unique, Navisworks and Other Ids
1144:Unique Id versus ElementId to Store in External Database
1144:Local Uniqueness of the Revit Unique Id
1144:Navisworks versus Revit Object Unique Ids
1144:Revit Id and UniqueId Lost On Reimporting Revised Model
1209:Unique Names and the NamingUtils Class
1277:Understanding the Use of the UniqueId
1304:Extracting Unique Building Element Geometry Vertices
1459:Consistency of IFC GUID and UniqueId
1577:<"#4">Unique IDs for Forge Viewer Elements
1634:RevitLookup Search by Element and Unique Id
1634:<"#2">Search and Snoop by Element Id or Unique Id
1949:Unique Id and IFC GUID Parameter
1949:<"#4"> You Cannot Control the Unique Id
1959:Immutable UniqueId and Revit Database Explorer
1959:<"#2"> Immutable UniqueId, Mutable Element Id
>>>> 0104 0737 0787 0819 0943 1144 1209 1277 1304 1459 1577 1634 1949 1959
<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/02/uniqueid-dwf-and-ifc-guid.html">UniqueId, DWF and IFC GUID</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/03/melbourne-day-two.html">Melbourne Day Two</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/06/real-world-concrete-corner-coordinates.html">Real-World Concrete Corner Coordinates</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/09/ifc-guid-generation-and-uniqueness.html">IFC GUID Generation and Uniqueness</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/05/copy-and-paste-api-applications-and-modeless-assertion.html">Copy and Paste API Applications and Modeless Assertion</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/04/element-id-export-unique-navisworks-and-other-ids.html">Element Id &ndash; Export, Unique, Navisworks and Other Ids</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/09/unique-names-and-the-namingutils-class.html">Unique Names and the NamingUtils Class</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/02/understanding-the-use-of-the-uniqueid.html">Understanding the Use of the UniqueId</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/04/back-from-easter-holidays-and-various-revit-api-issues.html">Back from Easter Holidays and Various Revit API Issues</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/08/consistency-of-ifc-guid-and-uniqueid.html">Consistency of IFC GUID and UniqueId</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/08/revit-versus-forge-ids-and-add-in-installation.html">Revit versus Forge, Ids and Add-In Installation</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2018/03/revitlookup-search-and-snoop-by-element-and-unique-id.html">RevitLookup Search by Element and Unique Id</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2022/04/unique-id-and-ifc-guid.html">Unique Id and IFC GUID Parameter</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2022/07/immutable-uniqueid-and-revit-database-explorer.html">Immutable UniqueId and Revit Database Explorer</a></li>
</ul>

####<a name="2"></a> sublime text

sublime text
https://thume.ca/2017/03/04/my-text-editor-journey-vim-spacemacs-atom-and-sublime-text/

####<a name="2"></a> BIM-GPT: a Prompt-Based Virtual Assistant Framework for BIM Information Retrieval

BIM-GPT: a Prompt-Based Virtual Assistant Framework for BIM Information Retrieval
https://arxiv.org/abs/2304.09333
> Efficient information retrieval (IR) from building information models (BIMs) poses significant challenges due to the necessity for deep BIM knowledge or extensive engineering efforts for automation. We introduce BIM-GPT, a prompt-based virtual assistant (VA) framework integrating BIM and generative pre-trained transformer (GPT) technologies to support NL-based IR. A prompt manager and dynamic template generate prompts for GPT models, enabling interpretation of NL queries, summarization of retrieved information, and answering BIM-related questions. In tests on a BIM IR dataset, our approach achieved 83.5% and 99.5% accuracy rates for classifying NL queries with no data and 2% data incorporated in prompts, respectively. Additionally, we validated the functionality of BIM-GPT through a VA prototype for a hospital building. This research contributes to the development of effective and versatile VAs for BIM IR in the construction industry, significantly enhancing BIM accessibility and reducing engineering efforts and training data requirements for processing NL queries.
bim_gpt_prompt_manager_nl_processing.png

####<a name="2"></a> interesting AI motivation: ensure it goes well:

interesting AI motivation: ensure it goes well:
Tristan Hume https://thume.ca/
All my favorite tracing tools: eBPF, QEMU, Perfetto, new ones I built and more
https://thume.ca/2023/12/02/tracing-methods/
> AI now is still missing a lot, but progress is incredibly fast. It’s hard for me to say the coming decade of progress won’t lead to AI as good as us at nearly all jobs, which would be the biggest event in history. Anthropic is unusually full of people who joined because they really care about ensuring this goes well. I think we have the world’s best alignment, interpretability research, and AI policy teams, and I personally work on performance optimization here because I think it’s the best way to leverage my comparative advantage to help the rest of our efforts succeed at steering towards AI going well for the world in the event it keeps up this pace.

####<a name="2"></a> aec greenwashing -- https://www.linkedin.com/pulse/can-construction-refrain-from-greenwashing-aarni-heiskanen-dqfuf/?midToken=AQFbSE8RCQ2a5g&midSig=3AqoWZGBDAwH41&trk=eml-email_series_follow_newsletter_01-newsletter_hero_banner-0-open_on_linkedin_cta&trkEmail=eml-email_series_follow_newsletter_01-newsletter_hero_banner-0-open_on_linkedin_cta-null-7371t~lr3k4lr6~ie-null-null&eid=7371t-lr3k4lr6-ie

aec greenwashing -- https://www.linkedin.com/pulse/can-construction-refrain-from-greenwashing-aarni-heiskanen-dqfuf/?midToken=AQFbSE8RCQ2a5g&midSig=3AqoWZGBDAwH41&trk=eml-email_series_follow_newsletter_01-newsletter_hero_banner-0-open_on_linkedin_cta&trkEmail=eml-email_series_follow_newsletter_01-newsletter_hero_banner-0-open_on_linkedin_cta-null-7371t~lr3k4lr6~ie-null-null&eid=7371t-lr3k4lr6-ie

####<a name="2"></a> Climpact carbon footprint action comparison

Climpact carbon footprint action comparison
[Climpact](https://climpact.ch)
developed at [EPFL](https://www.epfl.ch), the École Polytechnique Fédérale de Lausanne,
> Our actions have an impact on the climate.
Is our perception accurate?
Take our quiz to develop an intuition about how to reduce your carbon footprint, and to help us understand how people perceive the impact of their actions.

####<a name="2"></a> Now You See It, Now You Don't

Interesting proof that we perceive subtle details that we are completely unaware of is provided by research proving
that [Images altered to trick machine vision can influence humans too](https://deepmind.google/discover/blog/images-altered-to-trick-machine-vision-can-influence-humans-too/).

####<a name="2"></a> IKEA Life at Home Report

Talking about AEC, let's also think about the occupants, their thoughts, and wishes.

IKEA asked them, many of them, worldwide, and hazarded some projections into the future as well:

- [IKEA Life at Home Report 2023](https://lifeathome.ikea.com/the-2023-report/)
- [Life at Home Report 2023 PDF](https://lifeathome.ikea.com/wp-content/uploads/2024/01/Life-at-Home-Report-2023.pdf)
- Fast Company article
  on [Algae wallpaper, pod homes, and mushroom furniture: Ikea’s predictions for life in 2030 are semi-apocalyptic](https://www.fastcompany.com/91006861/algae-wallpaper-pod-housing-and-mushroom-furniture-ikeas-predictions-for-life-in-2030-are-semi-apocalyptic)
  > Ikea explores how our idea of home might evolve in a scarce future




####<a name="2"></a>

Thank you for that

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- Pixel Height: 848 Pixel Width: 2,598 -->
</center>
