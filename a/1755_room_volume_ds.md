<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


twitter:

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

-->

### Generate Room Volume DirectShape

This was prompted the following request:



####<a name="2"></a> Room Volume Representation in Forge SVF File

- Mustapha Bismi of [Vinci Facilities](https://www.vinci-facilities.com)
  Génération des volumes Revit
  Aujourd’hui, notre workflow consiste à prendre la géométrie des pièces Revit, générer des fichiers SAT, puis recréer des volumes Revit à partir de cette géométrie.
  Dans le cadre d’une automatisation, c’est pas terrible terrible.
The context: We are building digital twins out of BIM data. To do so, we use Revit, Dynamo, and Forge.
You can check what we do with that on our website: https://www.twinops.com/
The issue: We rely on the rooms in Revit to perform a bunch of tasks (reassign equipment localization, rebuild a navigation tree, and so on).
Unfortunately, theses rooms are not displayed in the Revit 3D view.
Therefore, they are nowhere to be found in the Forge SVF file.
Our (so-so) solution: The original solution was developed with Autodesk consulting.
We use Dynamo to extract the room geometry and build Revit volumes.
It works, but it is:
Not very robust: Some rooms has to be recreated manually, Dynamo crashes, geometry with invalid faces is produced, etc.
Not very fast: The actual script exports SAT files and reimports them.
Manual: Obviously, and also tedious and error-prone.
The whole process amounts to several hours of manual work.
We want to fix this.
Our goal: A robust implementation that will get rid of Dynamo, automate the process in Revit, and in the end, run that in a Forge Design Automation process.
The ideal way forward is exactly what you describe: A native C# Revit API that find the rooms, creates a direct shape volume for them, and copy their properties to that.
No intermediate formats, no UI, just straight automation work.

https://github.com/jeremytammik/RoomVolumeDirectShape




<center>
<img src="img/.png" alt="" width="100">
</center>


####<a name="3"></a> - Cherry BIM Services
  https://github.com/haninh2612/Batch-rename-Revit-type-name-with-custom-naming-convention
NINH TRUONG HUU HA
BIM Leader: Point Cloud to Revit expert, Revit API & Navisworks API developer, Solibri, RIB iTWO
Inspired by Jeremy Tammik and @Harry Mattison who always share their incredible knowledge to the world, I decided from now on, all of my Revit add in will be free to use for all Revit users. 
Start from my first add in: Batch Rename Revit Type name with Naming convention. 
You can download and use at: https://lnkd.in/fGmjfTx 
If you like my add in and want to support me, please donate me at: https://lnkd.in/fjhXHJZ
Thank you and I am looking forward to received more feedback to enhance this tool.
Please share for other if you think it useful.
Source code on Github: https://lnkd.in/fRpX6JC
#revitapi #revit 
https://lnkd.in/fxE3ZAg
Jeremy Tammik: Thank you for sharing your add-in! Have you also submitted this to the AppStore? That will make it more accessible and visible to all Revit users...
NINH: Yes, I am submitting my code to Autodesk store as well. Hopefully to publish soon. :D
Jeremy: Is the source code also open source? Maybe on GitHub?
NINH: I haven't creat account on GitHub yet but soon I will.
Jeremy: you will love it!
NINH: :D I just upload my code to Github: https://github.com/haninh2612/Batch-rename-Revit-type-name-with-custom-naming-convention
Jeremy: i looked at your github repository and am very confused indeed. it contains a lot more material that has nothing to do with this add-in. i cannot even find the relevant code section for this add-in off-hand...
i would suggest cleaning up and removing all the junk before publishing this code  :-)

One year ago, I had absolutely zero knowledge of the coding world, e.g., C#, Revit API, Visual Studio, etc.
I would never have thought that someday I could have my own Revit add-in published in the Autodesk Store.
Fully free for every one:
[Warning Manager by Cherry BIM Services](https://apps.autodesk.com/RVT/en/Detail/Index?id=7980350830610368901&appLang=en&os=Win64)
Full credit to Jeremy Tammik and Harry Mattison, my Internet teachers that helped me all the way by their amazing blogs.
Description:
The warning message has one of the biggest impacts on the Autodesk® Revit® model quality and speed. When you have a lot of warnings it slows you down dramatically.
The default warning list of Revit is currently not efficient enough to show the errors to fix. This new tool will help you to manage all warning messages on Revit models with Isolate and create a Bounding box around the error Elements. 
The number of error types is also displayed in a built-in Pie chart to help you quickly understand which type of major error on your model you need to focus more. 
Supports Revit 2018; 2019; 2020

[Cherry BIM Services (CBS)](https://apps.autodesk.com/en/Publisher/PublisherHomepage?ID=WMJ4MMYLCSTR) is
a professional BIM freelancer team located in Ho Chi Minh city, Vietnam. We are providing Building Information Modeling services, consultancy services implement for designing and engineering projects. Our team member have many years experience working for the top BIM firms such as  COWI A/S; ARUP Group; Royal Haskoning DHV...working with many BIM projects around the world with different type of BIM specification and requirements.
As a major designing and engineering solutions provider, Cherry BIM Services (CBS) offers a range of services to clients, which include the following:
Scan to BIM: Convert Point Clouds to Revit models and 2D CAD drawings 
Custom Revit API: Automate your tasks with custom Revit add in based on your needs and support for all Revit versions
- Consulting Services and Custom Development
- One-minute demo video of [Auto-generate curtain grids free Revit add-in](https://youtu.be/Sacd3K6RBbU)
- [Auto curtain wall Dropbox download](https://www.dropbox.com/sh/rfllne68zjjjq9t/AAA7eLI-p1LqFHkRj3fBlxpza?dl=0)

- Cherry BIM Services
Batch Upgrade models, template, families for Revit 2020, Revit 2019, Revit 2018 and Revit 2017
https://youtu.be/rciLWaik2_0
<iframe width="560" height="315" src="https://www.youtube.com/embed/rciLWaik2_0" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
Fill out following form to download the add in: https://forms.gle/Bw6Ga8rcB5s42Jey5 
Upgrade Revit models. templates or Families are heavy tasks that we have to do everytime new Revit  version released. In order to save your time, I created this add in to do all the converting to current Revit version for you. Simply add all the files you want to upgrade and take some coffee until the add in do the rest. 
This add in support for Revit 2020, Revit 2019 , Revit 2018 and Revit 2017.
Boost your BIM published its upgrader in the AppStore

<pre class="code">
</pre>


For the full Visual Studio solution and updates to the code, please refer to
the [ GitHub repository](https://github.com/jeremytammik/).


####<a name="6"></a> Sample Model and Results

Sample model 3D view:

![Sample model 3D view](img/section_cut_geo_3d.png)

Plan view showing section location:

![Plan view showing section location](img/section_cut_geo_plan.png)

Cut geometry in section view:

![Cut geometry in section view](img/section_cut_geo_cut.png)

Model lines representing the cut geometry of the window family instance produced by the add-in in section view:

![Model lines representing cut geometry in section view](img/section_cut_geo_cut_geo_window.png)

Model lines representing the cut geometry of walls, door and window isolated in 3D view:

![Model lines representing cut geometry isolated in 3D view](img/section_cut_geo_cut_geo_3d.png)

Listing the number of processed elements, geometry objects and curves actually lying in the cut plane for this sample:

<pre>
Rooms...
</pre>

####<a name="7"></a> Challenges Encountered Underway


---------------------------
Autodesk Revit 2020
---------------------------
Licensing System Error 22 
Failed to locate Adls
---------------------------
OK   
---------------------------

Licensing System Error 22 Failed to locate Adls

https://forums.autodesk.com/t5/installation-licensing/error-de-sistema-de-licencias-22-failed-to-locate-adls/td-p/8771037

run Services.msc
search for Autodesk Desktop Licensing Service
start the service

####<a name="7"></a> Valid Direct Shape Categories 

Rooms is not acceeptable, generic model and strucutral framing is.


Autodesk.Revit.Exceptions.ArgumentException was unhandled by user code
  HResult=-2146233088
  Message=Element id categoryId may not be used as a DirectShape category.
Parameter name: categoryId
  ParamName=categoryId
  Source=RevitAPI
  StackTrace:
       at Autodesk.Revit.DB.DirectShape.CreateElement(Document document, ElementId categoryId)
       at RoomVolumeDirectShape.Command.Execute(ExternalCommandData commandData, String& message, ElementSet elements)
       at apiManagedExecuteCommand(AString* assemblyName, AString* className, AString* vendorDescription, MFCApp* pMFCApp, DBView* pDBView, AString* message, Set<ElementId\,std::less<ElementId>\,tnallc<ElementId> >* ids, Map<AString\,AString\,std::less<AString>\,tnallc<std::pair<AString const \,AString> > >* data, AString* exceptionName, AString* exceptionMessage)
  InnerException: 


{"Actual Lighting Load per area(r)" : "0", "Actual Lighting Load(r)" : "0", "Actual Power Load per area(r)" : "0", "Actual Power Load(r)" : "0", "Area per Person(r)" : "1139.04", "Area(r)" : "15.03", "Base Finish(s)" : "", "Base Lighting Load on(n)" : "-1", "Base Offset(r)" : "0", "Base Power Load on(n)" : "-1", "Calculated Cooling Load per area(r)" : "0", "Calculated Heating Load per area(r)" : "0", "Calculated Supply Airflow per area(r)" : "0", "Category(e)" : "-2000160", "Ceiling Finish(s)" : "", "Comments(s)" : "", "Computation Height(r)" : "0", "Department(s)" : "", "Design Option(e)" : "-1", "Family Name(s)" : "", "Floor Finish(s)" : "", "Heat Load Values(n)" : "-1", "Image(e)" : "-1", "Latent Heat Gain per person(r)" : "630.92", "Level(e)" : "245423", "Level(s)" : "Level 2", "Lighting Load Units(n)" : "0", "Limit Offset(r)" : "21.33", "Name(s)" : "Linen", "Number of People(r)" : "0", "Number(s)" : "208", "Occupancy Unit(n)" : "0", "Occupancy(s)" : "", "Occupant(s)" : "", "Perimeter(r)" : "19.44", "Phase Id(e)" : "86961", "Phase(e)" : "86961", "Plenum Lighting Contribution(r)" : "0.2", "Power Load Units(n)" : "0", "Sensible Heat Gain per person(r)" : "788.65", "Specified Lighting Load per area(r)" : "10.76", "Specified Lighting Load(r)" : "0", "Specified Power Load per area(r)" : "10.76", "Specified Power Load(r)" : "0", "Total Heat Gain per person(r)" : "1419.57", "Type Name(s)" : "", "Unbounded Height(r)" : "21.33", "Upper Limit(e)" : "245423", "Volume(r)" : "235.65", "Wall Finish(s)" : ""}

Structural Framing

Matthias Stark 089 547 69 095

####<a name="7"></a> Direct Shape Phase and Visibility

The Direct shepe elements were visible in RevitLookup, and all their properties looked fine.

However, try as I might, I was unable to see them in the Revit 3D view...

...until I finally flipped through the phases and found the right one.

Weird setup in the rac basic sample model.


####<a name="7"></a> Caveat

This sample currently only handles solid and instance geometry objects.

There may well be other object types that need to be handled as well to provide full coverage for all situations.


####<a name="7"></a> AI-Generated Talking Head Models

  [Few-Shot Adversarial Learning of Realistic Neural Talking Head Models](https://youtu.be/p1b5aiTrGzY)
  <iframe width="560" height="315" src="https://www.youtube.com/embed/p1b5aiTrGzY" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
  
  

**Question:** 
