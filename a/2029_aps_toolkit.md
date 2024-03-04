<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- https://highlightjs.org/#usage
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
-->

<!-- https://prismjs.com -->
<link href="https://www.jsdelivr.com/prismjs@v1.x/themes/prism.css" rel="stylesheet" />
<script src="https://www.jsdelivr.com/prismjs@v1.x/components/prism-core.min.js"></script>
<script src="https://www.jsdelivr.com/prismjs@v1.x/plugins/autoloader/prism-autoloader.min.js"></script>
</head>

<!---

- https://www.linkedin.com/posts/chuongmep_ai-bim-aps-activity-7167851379355533313-hs3r?utm_source=share&utm_medium=member_desktop
  Chuong HoChuong Ho
  Computational Design Researcher | Autodesk Expert Elite | ConsultantComputational Design Researcher | Autodesk Expert Elite | Consultant
  https://chuongmep.com/
  I am excited to announce a significant development in data interaction and retrieval processes using Autodesk Platform Services from Autodesk. Today, I am officially releasing the first version of a toolkit designed to facilitate data access, aiming to support AI processes, Data Analysts, LLM, and explore the boundaries where APS may fall short in providing for end-users.
  This toolkit is open-source, ensuring accessibility to all engineers, BIM developers, and data scientists. I am actively working on refining it further. Please feel free to provide any feedback in the comments below this post, and I will consider all suggestions.
  Open Source: https://lnkd.in/ghkv_BhM
  #AI #BIM #APS #Automation #LLM #DataAnalysis #OpenSource

- Easy Revit API
  https://easyrevitapi.com/

- The killer app of Gemini Pro 1.5 is video
  https://simonwillison.net/2024/Feb/21/gemini-pro-video/

- Generative AI exists because of the transformer
  https://ig.ft.com/generative-ai/
  a beginner's guide to understanding LLM

- White House urges developers to dump C and C++
  https://www.infoworld.com/article/3713203/white-house-urges-developers-to-dump-c-and-c.amp.html

- The harsh reality of ultra processed food - with Chris Van Tulleken
  https://youtu.be/5QOTBreQaIk

twitter:

 the #RevitAPI @AutodeskRevit #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### APS Toolkit



####<a name="2"></a> APS Toolkit

In the last post,
I mentioned [Chuong Ho](https://chuongmep.com/)'s
[BIM interactive notebooks](https://thebuildingcoder.typepad.com/blog/2024/02/interactive-bim-notebook-temporary-graphics-and-ai.html#2).

Now you can see how he put them to use in his newest project,
the [APS Toolkit](https://github.com/chuongmep/aps-toolkit):

APS Toolkit (Former is Forge) is powerful for you to explore `Autodesk Platform Services`(APS). It's built on top of [Autodesk.Forge](https://www.nuget.org/packages/Autodesk.Forge/) and [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/). Forge Toolkit includes some features allow you to read, download and write data from `Autodesk Platform Services` and export to CSV, Excel, JSON, XML, etc.

![APSToolkit](docs/APSToolkit.png)

## ⚡ Features

- [x] Read/Download SVF Model
- [x] Read/Query Properties Database SQLite
- [x] Read/Download Properties Without Viewer
- [x] Read Geometry Data
- [x] Read Metadata
- [x] Read Fragments
- [x] Read MeshPacks
- [x] Read Images
- [x] Export Data to CSV
- [x] Export Data to Excel
- [x] Export Data to Parquet

Sample usage to export Revit Data To Excel using .NET C&#35;:

<pre><code class="language-csharp">
using APSToolkit;
using Autodesk.Forge;
using APSToolkit.Database;
using APSToolkit.Auth;
var token = Authentication.Get2LeggedToken().Result;
string urn = "dXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLk9kOHR4RGJLU1NlbFRvVmcxb2MxVkE_dmVyc2lvbj0z";
var RevitPropDbReader = new PropDbReaderRevit(urn, token);
RevitPropDbReader.ExportAllDataToExcel("result.xlsx");
</code></pre>

Sample usage to export Revit Data To Excel using Python:

<pre><code class="language-python">n
from aps_toolkit import Auth
from aps_toolkit import PropDbReaderRevit
auth = Auth()
token = auth.auth2leg()
urn = "dXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLk9kOHR4RGJLU1NlbFRvVmcxb2MxVkE_dmVyc2lvbj0z"
prop_reader = PropDbReaderRevit(urn, token)
df = prop_reader.get_data_by_category("Ducts")
df.save_to_excel("result.xlsx")
</code></pre>


<center>
<img src="img/" alt="" title="" width="100"/> <!-- Pixel Height: 656 Pixel Width: 748 -->
</center>

Many thanks to Chuong Ho for this!



<pre>
</pre>

**Question:**

**Answer:**

**Response:**


