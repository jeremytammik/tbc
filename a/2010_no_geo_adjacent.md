<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Get touching elements
  https://forums.autodesk.com/t5/revit-api-forum/get-touching-elements/m-p/12223781

- The BIM has No Geometry
  [Simplify elements geometry](https://forums.autodesk.com/t5/revit-api-forum/simplify-elements-geometry/m-p/12266629)
  **Question:** I am trying to simfplify the geometry of elements (such as cylinders, shperes, etc.) throgh the API.
  My first idea was simple:
  1. Retrieve a mesh from all faces with low quality using face.Triangulate(0);
  2. Apply the new mesh for element's geometry.
  However, it does not work. I have seen messages on the forum stating that working with geometry through the API is not allowed. Anyway, I believe that there it should be another approach to tackle this issue. I would appreciate any help.
  **Answer:** No. There is no way that you can specify Revit element geometry. Please bear in mind that Revit is a BIM authoring tool. The BIM is completely driven by parameters and constraints.
  There is no geometry in the BIM!
  The model as you see it (and its geometry) is just a view of the elements, their relationships, parameters and constraints.

- Revit and IFC coordinate systems
  [Revit, IFC and coordinate systems](https://bim-me-up.com/en/revit-ifc-und-koordinatensysteme/)
  Lejla Secerbegovic
  Digital Technology Enthusiast | BIM & Sustainability

- Deepfakes of Chinese influencers are livestreaming 24/7
  https://www.technologyreview.com/2023/09/19/1079832/chinese-ecommerce-deepfakes-livestream-influencers-ai/
  > With just a few minutes of sample video and $1,000 in costs, brands can clone a human streamer to work 24/7.

- generative ai use and misuse
  [How people can create &ndash; and destroy &ndash; value with generative AI](https://www.bcg.com/publications/2023/how-people-create-and-destroy-value-with-gen-ai)
  Key Takeaways &ndash; A first-of-its-kind scientific experiment finds that people mistrust generative AI in areas where it can contribute tremendous value and trust it too much where the technology isn’t competent:
  Around 90% of participants improved their performance when using GenAI for creative ideation. People did best when they did not attempt to edit GPT-4’s output.
  When working on business problem solving, a task outside the tool’s current competence, many participants took GPT-4's misleading output at face value. Their performance was 23% worse than those who didn’t use the tool at all.
  Adopting generative AI is a massive change management effort. The job of the leader is to help people use the new technology in the right way, for the right tasks and to continually adjust and adapt in the face of GenAI’s ever-expanding frontier.

twitter:

@AutodeskAPS  #RevitAPI  @AutodeskRevit  #BIM @DynamoBIM

linkedin:

 #AutodeskAPS  #Revit #API  #Autodesk

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Touching Geometry and No Geometry


<pre class="prettyprint lang-cs">

</pre>


<center>
<img src="img/" alt="" title="" width="100"/>
</center>


####<a name="2"></a> Get touching elements

Get touching elements
https://forums.autodesk.com/t5/revit-api-forum/get-touching-elements/m-p/12223781

####<a name="3"></a> The BIM has No Geometry

The BIM has No Geometry
[Simplify elements geometry](https://forums.autodesk.com/t5/revit-api-forum/simplify-elements-geometry/m-p/12266629)
**Question:** I am trying to simfplify the geometry of elements (such as cylinders, shperes, etc.) throgh the API.
My first idea was simple:
1. Retrieve a mesh from all faces with low quality using face.Triangulate(0);
2. Apply the new mesh for element's geometry.
However, it does not work. I have seen messages on the forum stating that working with geometry through the API is not allowed. Anyway, I believe that there it should be another approach to tackle this issue. I would appreciate any help.
**Answer:** No. There is no way that you can specify Revit element geometry. Please bear in mind that Revit is a BIM authoring tool. The BIM is completely driven by parameters and constraints.
There is no geometry in the BIM!
The model as you see it (and its geometry) is just a view of the elements, their relationships, parameters and constraints.

####<a name="4"></a> Revit and IFC coordinate systems

Revit and IFC coordinate systems
[Revit, IFC and coordinate systems](https://bim-me-up.com/en/revit-ifc-und-koordinatensysteme/)
Lejla Secerbegovic
Digital Technology Enthusiast | BIM & Sustainability

####<a name="5"></a> Deepfakes of Chinese influencers are livestreaming 24/7

Deepfakes of Chinese influencers are livestreaming 24/7
https://www.technologyreview.com/2023/09/19/1079832/chinese-ecommerce-deepfakes-livestream-influencers-ai/
> With just a few minutes of sample video and $1,000 in costs, brands can clone a human streamer to work 24/7.

####<a name="6"></a> generative ai use and misuse

generative ai use and misuse
[How people can create &ndash; and destroy &ndash; value with generative AI](https://www.bcg.com/publications/2023/how-people-create-and-destroy-value-with-gen-ai)
Key Takeaways &ndash; A first-of-its-kind scientific experiment finds that people mistrust generative AI in areas where it can contribute tremendous value and trust it too much where the technology isn’t competent:
Around 90% of participants improved their performance when using GenAI for creative ideation. People did best when they did not attempt to edit GPT-4’s output.
When working on business problem solving, a task outside the tool’s current competence, many participants took GPT-4's misleading output at face value. Their performance was 23% worse than those who didn’t use the tool at all.
Adopting generative AI is a massive change management effort. The job of the leader is to help people use the new technology in the right way, for the right tasks and to continually adjust and adapt in the face of GenAI’s ever-expanding frontier.


