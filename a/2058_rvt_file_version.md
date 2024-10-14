<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- https://highlightjs.org/#usage
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
-->

<!-- https://prismjs.com -->
<link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>
<style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style>

</head>

<!---

- read RVT without Revit API
  https://forums.autodesk.com/t5/revit-api-forum/has-any-method-can-replace-basicfileinfo-extract/m-p/8822648
  https://thebuildingcoder.typepad.com/blog/2017/06/determining-rvt-file-version-using-python.html#comment-4484205626
  [Unload Links with Transmission Data](https://thebuildingcoder.typepad.com/blog/2024/10/unload-links-offline-and-filter-for-types.html#5)
  https://autodesk.slack.com/archives/C0PLC20PP/p1728689603509229
  Ali Atabey
  Hi Team, a customer has the following question. I appriciate your guidance.
  We use Autodesk APIs to monitor model health via dashboards, but we're having trouble retrieving model version years and cloud GUIDs (e.g., 994d0d57-67d7-4288-a3e8-e9bce5d0cae5). Is there an easier way to access this data than using the Model Derivative API?
  Xiao Dong Liang
  could you ask the customer to elaborate the demo information that they are seeking for? Since they mentioned Model Derivative, I guess the info is Revit document info like attached demo. This is easiest to me, however, no idea why they still want to have easier way.
  If they do not want to use cloud services, which means they need to program plugin by Revit API, and depends on the machine has installed Revit.. which is obviously complicated..
  Or they want to Determining the RVT File version without Revit, either without uploading file to APS cloud (And use Model Derivative) , then they may check the article
  @Jeremy Tammik
   wrote, but I am not sure if it applies with latest Revit version.
  https://jeremytammik.github.io/tbc/a/0887_rvt_file_version.htm
  model-info.json
  {
      "Document Information": {
          "RVTVersion": "2022",
          "Project Name": "Project Name",
          "Project Number": "Project Number",
          "Author": "",
          "Project Address": "Enter address here",
          "Project Issue Date": "Issue Date",
          "Project Status": "Project Status",
          "Building Name": "",
          "Client Name": "Owner",
          "Organization Name": "",
          "Organization Description": ""
      },
      "selfDocumentIdentity": {
          "instanceId": "7f832a50-e1dc-4345-86e7-00acd3110a3a",
          "revitNumberOfSaves": 10
      }
  }
  Ali Atabey
  thank you for the quick response. I‘ll check with the customer. Should I CC you? They are new to APS, so it might just be inexperience.
  Eason Kang
  Just FYI, the Regx way in Jeremy’s blog post would not work for recent Revit versions (e.g. 2024, 2025) . A formal reliable way is to use Revit API ‘BasicFileInfo.Extract’ with Design Automation API for Revit instead, which doesn’t require opening the files.
  https://github.com/yiskang/DA4R-RevitBasicFileInfoExtract
  If I remember correctly, the regex method doesn’t return version info when testing with Revit 2024 models. (seemed due to scheme changes). A customer reported this issue through https://forge.zendesk.com/agent/tickets/17375
  Identifying Revit File Version
  After discussing with the customer, we used Revit API ‘BasicFileInfo.Extract’ with Design Automation API for Revit instead at the end to get the Revit file version year reliably.
  https://aps.autodesk.com/blog/check-version-revit-file-using-design-automation-api (edited)
  The DA approach is much more reliable based on my tests.
  I tried the Regex one with the Revit 2024 file. I spent the whole afternoon trying to find out why it didn’t work at that time, but I failed, and then I wrote the sample addin for DA using Revit API ‘BasicFileInfo.Extract’.
  Xiao Dong Liang
  Thanks Eason! so it sounds it has to go with plugin for latest version of Revit files. which means Revit has to be installed..
  Eason Kang
  With Design Automation API, the customer should not have to install Revit on his machine, just need to run the workitem, right? :thinking_face: The Revit 2025 version can read RVT version equals and lower than 2025. So, we don’t need to make an app bundle for each Revit version.
  Xiao Dong Liang
  this customer thinks Model Derivative is even not easy. I think they would not like to try with the more complicated Design Automation :grimacing:
  Ali Atabey
  Thank you both for the information. I'll pass it on to the customer. As I mentioned earlier, they are architects who are new to this process and learning as they go. This information will be very valuable to them.

- Tesla's 'We, Robot' Event: Everything Revealed in 8 Minutes
  https://youtu.be/Mu-eK72ioDk
  > At Tesla's 'We, Robot' event in Los Angeles, CEO Elon Musk unveils Robotaxi,  a fully autonomous car for less than $30,000, Robovan, a 20 passenger vehicle, and new updates to its humanoid robot, Optimus, for less than the cost of a car.

- State of AI Report 2024
  https://www.stateof.ai/

- AI Will Not Destroy The World—AI Illiteracy And Misuse Could
  https://www.forbes.com/sites/luisromero/2024/10/08/ai-will-not-destroy-the-world-ai-illiteracy-and-misuse-could/

- nobel prize
  John J. Hopfield and Geoffrey E. Hinton trained artificial neural networks using physics
  https://www.nobelprize.org/prizes/physics/2024/press-release/
  They used physics to find patterns in information
  https://www.nobelprize.org/uploads/2024/10/popular-physicsprize2024-2.pdf
  Scientific Background to the Nobel Prize in Physics 2024: “For Foundational Discoveries And Inventions That Enable Machine Learning With Artificial Neural Networks”
  https://www.nobelprize.org/uploads/2024/09/advanced-physicsprize2024.pdf

twitter:

 the @AutodeskRevit #RevitAPI #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
<a href="img/.gif"><p style="font-size: 80%; font-style:italic">Click for animation</p></a>
</center>

-->

### Determine RVT Version


####<a name="2"></a>

**Question:**

**Answer:**


<pre><code class="language-cs"></code></pre>

<center>
<img src="img/.png" alt="" title="" width="458"/> <!-- Pixel Height: 600 Pixel Width: 458 -->
</center>


**Response:**


Many thanks to

for raising the issue and sharing their solution.


####<a name="2"></a> Determine RVT Version

https://forums.autodesk.com/t5/revit-api-forum/has-any-method-can-replace-basicfileinfo-extract/m-p/8822648
https://thebuildingcoder.typepad.com/blog/2017/06/determining-rvt-file-version-using-python.html#comment-4484205626
[Unload Links with Transmission Data](https://thebuildingcoder.typepad.com/blog/2024/10/unload-links-offline-and-filter-for-types.html#5)
https://autodesk.slack.com/archives/C0PLC20PP/p1728689603509229
Ali Atabey
Hi Team, a customer has the following question. I appriciate your guidance.
We use Autodesk APIs to monitor model health via dashboards, but we're having trouble retrieving model version years and cloud GUIDs (e.g., 994d0d57-67d7-4288-a3e8-e9bce5d0cae5). Is there an easier way to access this data than using the Model Derivative API?
Xiao Dong Liang
could you ask the customer to elaborate the demo information that they are seeking for? Since they mentioned Model Derivative, I guess the info is Revit document info like attached demo. This is easiest to me, however, no idea why they still want to have easier way.
If they do not want to use cloud services, which means they need to program plugin by Revit API, and depends on the machine has installed Revit.. which is obviously complicated..
Or they want to Determining the RVT File version without Revit, either without uploading file to APS cloud (And use Model Derivative) , then they may check the article
@Jeremy Tammik
wrote, but I am not sure if it applies with latest Revit version.
https://jeremytammik.github.io/tbc/a/0887_rvt_file_version.htm
model-info.json
{
"Document Information": {
"RVTVersion": "2022",
"Project Name": "Project Name",
"Project Number": "Project Number",
"Author": "",
"Project Address": "Enter address here",
"Project Issue Date": "Issue Date",
"Project Status": "Project Status",
"Building Name": "",
"Client Name": "Owner",
"Organization Name": "",
"Organization Description": ""
},
"selfDocumentIdentity": {
"instanceId": "7f832a50-e1dc-4345-86e7-00acd3110a3a",
"revitNumberOfSaves": 10
}
}
Ali Atabey
thank you for the quick response. I‘ll check with the customer. Should I CC you? They are new to APS, so it might just be inexperience.
Eason Kang
Just FYI, the Regx way in Jeremy’s blog post would not work for recent Revit versions (e.g. 2024, 2025) . A formal reliable way is to use Revit API ‘BasicFileInfo.Extract’ with Design Automation API for Revit instead, which doesn’t require opening the files.
https://github.com/yiskang/DA4R-RevitBasicFileInfoExtract
If I remember correctly, the regex method doesn’t return version info when testing with Revit 2024 models. (seemed due to scheme changes). A customer reported this issue through https://forge.zendesk.com/agent/tickets/17375
Identifying Revit File Version
After discussing with the customer, we used Revit API ‘BasicFileInfo.Extract’ with Design Automation API for Revit instead at the end to get the Revit file version year reliably.
https://aps.autodesk.com/blog/check-version-revit-file-using-design-automation-api (edited)
The DA approach is much more reliable based on my tests.
I tried the Regex one with the Revit 2024 file. I spent the whole afternoon trying to find out why it didn’t work at that time, but I failed, and then I wrote the sample addin for DA using Revit API ‘BasicFileInfo.Extract’.
Xiao Dong Liang
Thanks Eason! so it sounds it has to go with plugin for latest version of Revit files. which means Revit has to be installed..
Eason Kang
With Design Automation API, the customer should not have to install Revit on his machine, just need to run the workitem, right? :thinking_face: The Revit 2025 version can read RVT version equals and lower than 2025. So, we don’t need to make an app bundle for each Revit version.
Xiao Dong Liang
this customer thinks Model Derivative is even not easy. I think they would not like to try with the more complicated Design Automation :grimacing:
Ali Atabey
Thank you both for the information. I'll pass it on to the customer. As I mentioned earlier, they are architects who are new to this process and learning as they go. This information will be very valuable to them.

####<a name="2"></a> Tesla's Automamous Vehicles and Robots

Tesla's 'We, Robot' Event: Everything Revealed in 8 Minutes
https://youtu.be/Mu-eK72ioDk

> At Tesla's 'We, Robot' event in Los Angeles, CEO Elon Musk unveils Robotaxi,  a fully autonomous car for less than $30,000, Robovan, a 20 passenger vehicle, and new updates to its humanoid robot, Optimus, for less than the cost of a car.

####<a name="2"></a> State of AI Report 2024

State of AI Report 2024
https://www.stateof.ai/

####<a name="2"></a> AI Illiteracy AND Misuse

- AI Will Not Destroy The World—AI Illiteracy And Misuse Could
https://www.forbes.com/sites/luisromero/2024/10/08/ai-will-not-destroy-the-world-ai-illiteracy-and-misuse-could/

####<a name="2"></a> nobel prize for Science

- nobel prize
John J. Hopfield and Geoffrey E. Hinton trained artificial neural networks using physics
https://www.nobelprize.org/prizes/physics/2024/press-release/
They used physics to find patterns in information
https://www.nobelprize.org/uploads/2024/10/popular-physicsprize2024-2.pdf
Scientific Background to the Nobel Prize in Physics 2024: “For Foundational Discoveries And Inventions That Enable Machine Learning With Artificial Neural Networks”
https://www.nobelprize.org/uploads/2024/09/advanced-physicsprize2024.pdf

####<a name="2"></a> The Techno-Pro Attitude

Let's also look critically at scientific progress

- The Techno-Pro Attitude
  https://cacm.acm.org/article/do-all-problems-have-technical-fixes/
  Robin K. Hill suggests we look at the underlying presumption of the technology imperative.
  philosophisches traktat

####<a name="2"></a> Jevons Paradox

[Jevons paradox](https://en.wikipedia.org/wiki/Jevons_paradox) occurs when technological progress increases the efficiency with which a resource is used (reducing the amount necessary for any one use), but the falling cost of use induces increases in demand enough that resource use is increased, rather than reduced.
Governments, both historical and modern, typically expect that energy efficiency gains will lower energy consumption, rather than expecting the Jevons paradox.
Right now, Switzerland is about to vote on increasing motorway capacity, possibly heading straight into another example of Jevons paradox.

