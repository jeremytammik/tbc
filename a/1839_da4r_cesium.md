<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- forge zen 6232 [Autodesk design automation Revit , text file as input]
  https://stackoverflow.com/questions/61395452/autodesk-design-automation-revit-text-file-as-input

- Logging in Revit Design Automation add-in
  https://stackoverflow.com/questions/61384581/logging-in-revit-design-automation-add-in
  `SystemConsole.WriteLine` output is captures in the workitem's `report.txt`.

- Custom properties using Design Automation
  https://forge.autodesk.com/blog/custom-properties-using-design-automation
  DA4R sample to extract Revit properties from BIM360 and add them as custom properties to the Forge viewer
  https://github.com/augustogoncalves/forge-customproperty-revit

- Upgrade version of Revit files with Design Automation
  https://github.com/Autodesk-Forge/forge-upgradefiles-revit
  This sample demostrated how to upgrade Revit project/family/template to the latest version using Design Automation for Revit API, including upgrade one file or one folder.

- CesiumJS Open Source 3D Mapping
  https://www.cesium.com/cesiumjs/
  https://www.cesium.com/docs/tutorials/getting-started/

- banksy: My wife hates it when I work from home.
  https://www.instagram.com/p/B_Aqdh4Jd5x/?utm_source=ig_web_button_share_sheet

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

### DA4R 

Forge Design Automation for Revit or DA4R is really starting to take off.

Let's take a look at some recent topicw that came up:


####<a name="2"></a> Display Custom Properties using Design Automation

Augusto [@augustomaia](https://twitter.com/augustomaia) Goncalves published a nice article
on [custom properties using Design Automation](https://forge.autodesk.com/blog/custom-properties-using-design-automation) showing
how to implement a Forge viewer extension that displays standard Revit data already available in the RVT model but not handled by the standard Forge translation using DA4R to extract the Revit properties from BIM360 and add them as custom properties to the Forge viewer.

The sample code is provided in
the [forge-customproperty-revit GitHub repository](https://github.com/augustogoncalves/forge-customproperty-revit).

<center>
<img src="img/ag_da4r_custom_properties.gif" alt="Custom properties displaying RVT data using DA4R" title="Custom properties displaying RVT data using DA4R" width="800"/>
</center>



####<a name="3"></a> Upgrade Revit File Version with Design Automation

Another recent Forge sample by Zhong [@johnonsoftware](https://twitter.com/johnonsoftware) Wu,
[forge-upgradefiles-revit](https://github.com/Autodesk-Forge/forge-upgradefiles-revit),
demostrates how to upgrade Revit project, family and template files to the latest version using Design Automation for Revit, including support for either individual files or entire folder.

<center>
<img src="img/zw_forge_upgradefiles_revit.png" alt="Custom properties displaying RVT data using DA4R" title="Custom properties displaying RVT data using DA4R" width="800"/>
</center>

####<a name="4"></a> Logging in a Revit Design Automation Add-In

In the StackOverflow discussion 
on [logging in Revit Design Automation add-in](https://stackoverflow.com/questions/61384581/logging-in-revit-design-automation-add-in),
Rahul Bhobe clarifies that all the add-in's `SystemConsole.WriteLine` output is captured in the running workitem's `report.txt`.

####<a name="5"></a> DA4R input and Output Files

Another StackOverflow question asks
[how to pass a text file as input to DA4R](https://stackoverflow.com/questions/61395452/autodesk-design-automation-revit-text-file-as-input):

**Question:** The Revit API I developed takes 3D point data from a text file as input and creates adaptive components RVT family files and eventually complex geometry by placing them on the right point coordinates.

The text file looks like this:

<pre>
  1.002, 20,502, 21.706
  12.502, 5,502, 7.706
  21.002, 15,502, 14.706
  ...
</pre>

I am basically reading the text data as input for creating the adaptive component family and then creating complex geometry by placing the adaptive components. Now, if I want to convert the add-in to use the Design automation API, I guess I will not be able to use text file as input.

My question is, what type of input file should I use to pass theb 3D point coordinates described above? Should it be JSON? If it needs to be JSON, then how I should write it to represent these point coordinates? 

**Answer:** The slightly more complex question is how to generate multiple output files.

That is answered by the article
on [how to generate dynamic number of output with Design Automation for Revit V3](https://forge.autodesk.com/blog/how-generate-dynamic-number-output-design-automation-revit-v3).

In passing, it also mentions multiple input files, saying:

> ... For the zipped input file, it's well documented at https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit/step6-post-workitem/, but for the output zipped result, it's not so clear...?

Trying to follow that link, I note that it is out of date.

The updated link that worked for me is [forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit/step7-post-workitem](https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit/step7-post-workitem).
Apparently, a new step was added to the tutorial after publishing the original article.

Looking at the Forge documentation and [additional notes on input arguments](https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit/step7-post-workitem/#additional-notes),
I see the instructions on how to pass JSON input data directly in the workitem itself.

I would assume that you can also use a different prefix instead of `data:application/json` such as `data:application/text` to pass in the data in its current form.

Please try that out and let us know how it works for you.

Alternatively, you can just stay on the safe side and convert your text data to JSON format.

There are innumerable ways of doing so.

The most minimalistic and simple would look like this:

<pre>
  [1.002, 20,502, 21.706,
  12.502, 5,502, 7.706,
  21.002, 15,502, 14.706,
  ...]
</pre>
  
That represents on single array of doubles.

A slightly more structured approach might be to pass in an array of triples of doubles like this:

<pre>
  [[1.002, 20,502, 21.706],
  [12.502, 5,502, 7.706],
  [21.002, 15,502, 14.706],
  ...]
</pre>

As you see, it is not hard.

**Response:** Many Thanks Jeremy. All nice help. I am going through it. I will make it JSON, to be on the safe side :) 

####<a name="6"></a> CesiumJS Open Source 3D Mapping

Moving away from the Revit API and DA4R, I stumbled across this very powerful and beautiful 3D world-wide mapping platform that I want to share with you:

- [CesiumJS](https://www.cesium.com/cesiumjs) &ndash; an open source JavaScript library for creating world-class 3D globes and maps with the best possible performance, precision, visual quality, and ease of use.

Take a look at the [getting started introduction](https://www.cesium.com/docs/tutorials/getting-started/) to get a feel for it.

####<a name="7"></a> banksy: My wife hates it when I work from home.

Let's round off with something non-technical, a new contribution
from [Banksy, saying, My wife hates it when I work from home](https://www.instagram.com/p/B_Aqdh4Jd5x/?utm_source=ig_web_button_share_sheet



####<a name="2"></a> DA4R Question? Join Accelerator!


**Question:** 

**Answer:** 

Your best bet for getting a reliable answer is to ask through
the [regular Forge help channels](https://forge.autodesk.com/en/support/get-help).

Better still, another approach yet more effective would be
to [join a Forge accelerator](https://forge.autodesk.com/accelerator-program) and ask there.

Since they are virtual nowadays, more people can participate.

That will guarantee you both an answer to your question and ensure you have ongoing support for the proof of concept.

You might even get your whole application completed right away during the accelerator.

How does that sound to you?

**Response:** 



####<a name="3"></a> 
