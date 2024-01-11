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


####<a name="2"></a> Journal Files and Logging

logging:
21768403 [Assistance Required with Revit Journal Files]
https://forums.autodesk.com/t5/revit-api-forum/api-access-to-user-history-quot-show-history-quot/m-p/12472116
AdWindows -- https://forums.autodesk.com/t5/revit-api-forum/intercepting-commands-executed-by-keyboard-shortcuts/td-p/12457597
https://forums.autodesk.com/t5/revit-api-forum/the-syntax-and-structure-of-the-journal-file-in-revit/m-p/12490089

####<a name="2"></a> WPF Progress Bar with Abort Button

Various solutions are discussed in the question
on [a working implementation of WPF progress bar with abort button](https://forums.autodesk.com/t5/revit-api-forum/can-anyone-share-a-working-implementation-of-wpf-progress-bar/m-p/12427800).
A promising one is provided by
the [SpeedCAD ProgressMeter](https://github.com/SpeedCAD/SCADtools.Revit.UI.ProgressMeter)
that [we recently discussed](https://thebuildingcoder.typepad.com/blog/2023/11/net-core-preview-and-open-source-add-in-projects.html#7).

####<a name="2"></a> Sublime Text

I mentioned a year ago that I switched to
the [Sublime Text](https://thebuildingcoder.typepad.com/blog/2023/02/pyramid-builder-commandloader-et-al.html#8) editor.

I am still using it and am perfectly satisfied with my choice.
It was nice to note that I am in the good company of [Tristan Hume](https://thume.ca/),
who documented his own experiences
in [my text editor journey: Vim, Spacemacs, Atom and Sublime Text](https://thume.ca/2017/03/04/my-text-editor-journey-vim-spacemacs-atom-and-sublime-text/).

####<a name="2"></a> Interesting AI Motivation: Ensure It Goes Well

I browsed Tristan's very interesting web site and was impressed by the compelling motivation to to get involved with AI development that he expresses in his discussion
of tracing methods,
[all my favorite tracing tools: eBPF, QEMU, Perfetto, new ones I built and more](https://thume.ca/2023/12/02/tracing-methods/):

> AI now is still missing a lot, but progress is incredibly fast.
It’s hard for me to say the coming decade of progress won’t lead to AI as good as us at nearly all jobs, which would be the biggest event in history.
Anthropic is unusually full of people who joined because they really care about ensuring this goes well.
I think we have the world’s best alignment, interpretability research, and AI policy teams, and I personally work on performance optimization here because I think it’s the best way to leverage my comparative advantage to help the rest of our efforts succeed at steering towards AI going well for the world in the event it keeps up this pace.

####<a name="2"></a> BIM-GPT: a Prompt-Based Virtual Assistant Framework for BIM Information Retrieval

To ensure that things go well in the BIM domain, AI-assisted information retrieval might help; maybe something
like [BIM-GPT, a prompt-based virtual assistant framework for BIM information retrieval](https://arxiv.org/abs/2304.09333):

> Efficient information retrieval (IR) from building information models (BIMs) poses significant challenges due to the necessity for deep BIM knowledge or extensive engineering efforts for automation. We introduce BIM-GPT, a prompt-based virtual assistant (VA) framework integrating BIM and generative pre-trained transformer (GPT) technologies to support NL-based IR. A prompt manager and dynamic template generate prompts for GPT models, enabling interpretation of NL queries, summarization of retrieved information, and answering BIM-related questions. In tests on a BIM IR dataset, our approach achieved 83.5% and 99.5% accuracy rates for classifying NL queries with no data and 2% data incorporated in prompts, respectively. Additionally, we validated the functionality of BIM-GPT through a VA prototype for a hospital building. This research contributes to the development of effective and versatile VAs for BIM IR in the construction industry, significantly enhancing BIM accessibility and reducing engineering efforts and training data requirements for processing NL queries.

<center>
<img src="img/bim_gpt_prompt_manager_nl_processing.png" alt="BIM-GPT NL query prompt processing" title="BIM-GPT NL query prompt processing" width="800"/> <!-- Pixel Height: 605 Pixel Width: 900 -->
</center>

####<a name="2"></a> AEC Greenwashing

Aarni Heiskanen shared an interesting article on the imminent danger
of [AEC greenwashing](https://www.linkedin.com/pulse/can-construction-refrain-from-greenwashing-aarni-heiskanen-dqfuf/).

####<a name="2"></a> IKEA Life at Home Report

Talking about AEC and sustainability, let's also think about the occupants, their thoughts and wishes.

IKEA asked them, in large numbers, worldwide, compiled the results and hazarded some projections into the future as well:

- [IKEA Life at Home Report 2023](https://lifeathome.ikea.com/the-2023-report/)
- [Life at Home Report 2023 PDF](https://lifeathome.ikea.com/wp-content/uploads/2024/01/Life-at-Home-Report-2023.pdf)
- Fast Company article
  on [Algae wallpaper, pod homes, and mushroom furniture: Ikea’s predictions for life in 2030 are semi-apocalyptic](https://www.fastcompany.com/91006861/algae-wallpaper-pod-housing-and-mushroom-furniture-ikeas-predictions-for-life-in-2030-are-semi-apocalyptic)
  > Ikea explores how our idea of home might evolve in a scarce future

####<a name="2"></a> Climpact Carbon Footprint Action Comparison

A great possibility to test your carbon footprint understanding or learn more about the sometimes surprising emmissions produced by various actions and behaviour is provided by
the [Climpact](https://climpact.ch) tool,
developed at [EPFL](https://www.epfl.ch),
the École Polytechnique Fédérale de Lausanne, one of the main Swiss technical universities.

> Our actions have an impact on the climate.
Is our perception accurate?
Take our quiz to develop an intuition about how to reduce your carbon footprint, and to help us understand how people perceive the impact of their actions.

####<a name="2"></a> Now You See It, Now You Don't

Finally, an AI-related note on pattern recogniton and adversarial images to prevent it provides
interesting proof that we humans perceive subtle details that we are completely unaware of, showing
that [images altered to trick machine vision can influence humans too](https://deepmind.google/discover/blog/images-altered-to-trick-machine-vision-can-influence-humans-too/).

