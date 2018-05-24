<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- [Access to Revit from an external application](http://adn-cis.org/forum/index.php?topic=745)
  [Community of Autodesk programmers in the CIS](http://adn-cis.org)
  [Community of Autodesk programmers in the CIS forum](http://adn-cis.org/forum)
  [Revit API board](http://adn-cis.org/forum/index.php?board=24.0)
  [CADBIMDeveloper version of RevitExternalAccessDemo](https://github.com/CADBIMDeveloper/RevitExternalAccessDemo)
  [Victor's original RevitExternalAccessDemo](https://github.com/chekalin-v/RevitExternalAccessDemo)
  0853_revit_wcf_service.htm

- https://forums.autodesk.com/t5/revit-api-forum/room-walls-direction/m-p/7995914

- https://forums.autodesk.com/t5/revit-api-forum/how-do-i-get-all-the-outermost-walls-in-the-model/m-p/7998948

- https://stackoverflow.com/questions/50285764/how-to-get-the-value-a-room-parameter-which-was-defined-by-a-third-party

Drive Revit via a WCF service, wall directions and parameters in the #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/rvtwcfwalls

Exciting news from Russia and some mundane updates on other repetitive topics
&ndash; Driving Revit from a modeless context via a WCF service 
&ndash; Room walls' directions 
&ndash; Retrieving all exterior walls 
&ndash; Getting and setting a shared parameter value...

--->

### Drive Revit via a WCF Service, Wall Directions and Parameters

Exciting news from Russia, and some mundane updates on other repetitive topics:

- [Driving Revit from a modeless context via a WCF service](#2) 
    - [В собственных словах Александра](#3) 
    - [How to host a WCF service in Revit](#4) 
    - [Content](#5) 
    - [Starting the WCF service in Revit](#6) 
- [Room walls' directions](#7) 
- [Retrieving all exterior walls](#8) 
- [Getting and setting a shared parameter value](#9) 


####<a name="2"></a>Driving Revit from a Modeless Context via a WCF Service

Over the years, we explored numerous different ways
to [drive Revit from outside, from a modeless context, via the `Idling` and external events](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28).

The need for this kind of approach is diminishing as
the [Forge platform](https://autodesk-forge.github.io) gets closer towards adding support for Revit to
its [Design Automation API](https://developer.autodesk.com/en/docs/design-automation/v2/overview),
sometimes referred to
as [Revit I/O](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28b).

One popular method, widely used in Russian-speaking areas of the world, is
Victor Chekalin's solution
to [drive Revit through a WCF service](http://thebuildingcoder.typepad.com/blog/2012/11/drive-revit-through-a-wcf-service.html),
cf. his [RevitExternalAccessDemo on GitHub](https://github.com/chekalin-v/RevitExternalAccessDemo),
originally implemented for Revit 2013.

Unfortunately, it stopped working in the Revit 2018 or 2019 timeframe.

The problem was extensively debated in
the [community of Autodesk programmers in the CIS](http://adn-cis.org) and
its [discussion forum](http://adn-cis.org/forum), which includes
a lively [Revit API board](http://adn-cis.org/forum/index.php?board=24.0),
in the thread
on [access to Revit from an external application](http://adn-cis.org/forum/index.php?topic=745).

Now Alexander [@aignatovich](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478) Ignatovich, aka Александр Игнатович,
stepped in and fixed the problem in
his [CADBIMDeveloper fork of RevitExternalAccessDemo](https://github.com/CADBIMDeveloper/RevitExternalAccessDemo).

In Alexander's own words:


####<a name="3"></a>В Собственных Словах Александра

I recently started to answer queries in the Russian ADN forum.

There was a discussion about driving Revit from outside.

Several years ago, Victor developed a sample that starts a WCF service in Revit.

It stopped working in Revit 2019.
 
This is my exploration of the problem:
 
The source code relies on independent calling of WCF service methods processing an `OnIdling` event, e.g., in this service method:
 
<pre class="code">
  lock (Locker)
  {
    // Enque GetDocumentPath task:
    
    TaskContainer.Instance.EnqueueTask(GetDocumentPath); 
   
    // Waiting for invoking Monitor.Pulse(Locker)
    // somewhere in desired time interval
    
    Monitor.Wait(Locker, WaitTimeout);
  }
  return currentDocumentPath;
</pre>

The calling of `Monitor.Pulse` in task processing method:
 
<pre class="code">
  private void GetDocumentPath(UIApplication uiapp)
  {
    try
    {
      currentDocumentPath = uiapp.ActiveUIDocument.Document.PathName;
    }
    finally
    {
      lock (Locker)
      {
        Monitor.Pulse(Locker); // <- HERE!
      }
    }
  }
</pre>

Actually, this code branch is invoked in the `OnIdling` event handler.
 
This worked for years and still works fine in Revit 2017.

However, since Revit 2019 (or maybe Revit 2018), we are waiting forever at the call to `Monitor.Wait` in the line:
 
<pre class="code">
  Monitor.Wait(Locker, WaitTimeout);
</pre>

Within the `Wait` timeout period, there is no `OnIdling` event handler call.
 
So, I let the service host be opened in another thread:
 
<pre class="code">
private const string ServiceUrlHttp = "http://localhost:9001/RevitExternalService";
private const string ServiceUrlTcp = "net.tcp://localhost:9002/RevitExternalService";

// ...
 
public Result OnStartup(UIControlledApplication application)
{
  application.Idling += OnIdling;
  
  try
  {
    Task.Factory.StartNew(() =>
      {
        var serviceHost = new ServiceHost(
          typeof(RevitExternalService),
          new Uri(ServiceUrlHttp),
          new Uri(ServiceUrlTcp));
 
        serviceHost.Description.Behaviors.Add(
          new ServiceMetadataBehavior());
          
        serviceHost.AddServiceEndpoint(
          typeof(IRevitExternalService),
          new BasicHttpBinding(),
          ServiceUrlHttp);
          
        serviceHost.AddServiceEndpoint(
          typeof(IRevitExternalService),
          new NetTcpBinding(),
          ServiceUrlTcp);
          
        serviceHost.AddServiceEndpoint(
          typeof(IMetadataExchange),
          MetadataExchangeBindings.CreateMexHttpBinding(),
          "mex");
          
        serviceHost.Open();
        
      }, TaskCreationOptions.LongRunning);
  }
  catch (Exception ex)
  {
    //....
  }
  return Result.Succeeded;
}
</pre>

With that, it started working again.

I use `http` binding, so Revit should be started as administrator, or you should use
the [`ServiceModel` registration tool](https://docs.microsoft.com/en-us/dotnet/framework/wcf/servicemodelreg-exe) to register WCF service for specific application.
 
Look at my [forked RevitExternalAccessDemo repository](https://github.com/CADBIMDeveloper/RevitExternalAccessDemo).
 
I also was interested in why it stopped working.

Was it due to some Revit mechanism change or due to changes in .Net 4.7 framework?

I created a simple application that starts a WCF service, but real request processing is in separate thread
(I use the same `TaskContainer`):

<pre class="code">
public partial class Form1 : Form
{
  private const string ServiceUrlHttp
    = "http://localhost:9001/ExternalService";
    
  private const string ServiceUrlTcp
    = "net.tcp://localhost:9002/ExternalService";
 
  public Form1()
  {
    InitializeComponent();
  }
 
  private void Form1_Load(object sender, EventArgs e)
  {
    Task.Factory.StartNew(() =>
      {
        while (true)
        {
          OnIdling();
 
          Thread.Sleep(TimeSpan.FromSeconds(1));
        }
      }, TaskCreationOptions.LongRunning);
 
    var serviceHost = new ServiceHost(
      typeof(ExternalService),
      new Uri(ServiceUrlHttp),
      new Uri(ServiceUrlTcp));
 
    serviceHost.Description.Behaviors.Add(
      new ServiceMetadataBehavior());
      
    serviceHost.AddServiceEndpoint(
      typeof(IExternalService),
      new BasicHttpBinding(),
      ServiceUrlHttp);
      
    serviceHost.AddServiceEndpoint(
      typeof(IExternalService),
      new NetTcpBinding(),
      ServiceUrlTcp);
      
    serviceHost.AddServiceEndpoint(
      typeof(IMetadataExchange),
      MetadataExchangeBindings.CreateMexHttpBinding(),
      "mex");
      
    serviceHost.Open();
  }
 
  private static void OnIdling()
  {
    if (!TaskContainer.Instance.HasTaskToPerform)
      return;
 
    var task = TaskContainer.Instance.DequeueTask();
    task();
  }
}
</pre>

It works as expected, so this change is probably a side effect of some changes in Revit itself.

I forked Victor's repository and made this thing work again in Revit 2019.

For the sake of completeness, here is also the CADBIMDeveloper/RevitExternalAccessDemo GitHub repo documentation:


####<a name="4"></a>How to Host a WCF Service in Revit

This code shows how to deploy a WCF service in Autodesk Revit and call it from an external application.

This code initially was written by Victor Chekalin
for [Revit 2013](https://github.com/chekalin-v/RevitExternalAccessDemo).

However, it does not work in Revit 2019. There is a discussion in
the [adn-cis](http://adn-cis.org/forum/index.php?topic=745.new;topicseen#new) forum.

The latest changes make it work again.

####<a name="5"></a>Content

This solution contains 2 projects:

- RevitExternalAccessDemo &ndash; an external application for Autodesk Revit 2019
- RevitExternalAccessClient &ndash; client application,  a console app that calls WCF service methods

####<a name="6"></a>Starting the WCF Service in Revit

You should start Revit as administrator or use
the [ServiceModel Registration Tool](https://docs.microsoft.com/en-us/dotnet/framework/wcf/servicemodelreg-exe) to
register WCF.



####<a name="7"></a>Room Walls' Directions

Several similar questions on determining the direction that walls are facing have cropped up in the last few weeks.

Here is a recent one from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [room walls direction](https://forums.autodesk.com/t5/revit-api-forum/room-walls-direction/m-p/7995914):

**Question:** How to get the direction of the walls of a room?
I mean the interior side of wall is facing in what direction?
East, West, North, South, Northeast, Northwest, Southeast, Southwest, etc.

You can assume I am inside the room and I am looking at the wall.
I need to get this direction.

**Answer:** To start with, you can look at the `DirectionCalculation` Revit SDK sample and The Building Coder discussion of it 
on [south facing walls](http://thebuildingcoder.typepad.com/blog/2010/01/south-facing-walls.html).

Benoit provides some further suggestions on:

- Determining whether you are inside the room versus outside
- Determining each wall's angle to north

For the first, he suggests using the most simple solution: draw a semi-infinite line &ndash; a ray &ndash; from the point you are checking to a point on the face of the wall. Determine the number of times it crosses the walls of the room. If you cross them an even number, you are inside the room. Elseway, you are outside.

For the direction you need, draw a line between your position and a point of the face of the wall.
Then, compute the angle to the North. You have your direction.


####<a name="8"></a>Retrieving All Exterior Walls

While I am writing this, another related question popped up,
on [how to get all the outermost walls in the model](https://forums.autodesk.com/t5/revit-api-forum/how-do-i-get-all-the-outermost-walls-in-the-model/m-p/7998948):

**Question:** How do I get all the outermost walls in the model?

Here is a picture showing what I mean:

<center>
<img src="img/exterior_walls.png" alt="Exterior walls" width="434"/>
</center>

**Answer:** Just as in the previous answer, the `DirectionCalculation` Revit SDK sample and The Building Coder discussion of it 
on [south facing walls](http://thebuildingcoder.typepad.com/blog/2010/01/south-facing-walls.html) will
provide a good starting point for you.

It uses the built-in wall function parameter `FUNCTION_PARAM` to filter for exterior walls.

I updated the code presented there and added it
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) for you,
in the [CmdCollectorPerformance.cs module lines L293-L323](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdCollectorPerformance.cs#L293-L323).

First, we implement a predicate method `IsExterior` that checks this parameter value to determine whether a wall type is exterior or not:

<pre class="code">
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;Wall&nbsp;type&nbsp;predicate&nbsp;for&nbsp;exterior&nbsp;wall&nbsp;function</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
  <span style="color:blue;">bool</span>&nbsp;IsExterior(&nbsp;<span style="color:#2b91af;">WallType</span>&nbsp;wallType&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;p&nbsp;=&nbsp;wallType.get_Parameter(
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.FUNCTION_PARAM&nbsp;);
   
  &nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;p,&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;wall&nbsp;type&nbsp;&quot;</span>
  &nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;to&nbsp;have&nbsp;wall&nbsp;function&nbsp;parameter&quot;</span>&nbsp;);
   
  &nbsp;&nbsp;<span style="color:#2b91af;">WallFunction</span>&nbsp;f&nbsp;=&nbsp;(<span style="color:#2b91af;">WallFunction</span>)&nbsp;p.AsInteger();
   
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">WallFunction</span>.Exterior&nbsp;==&nbsp;f;
  }
</pre>

With that, you can retrieve all exterior walls with a filtered element collector like this:

<pre class="code">
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;all&nbsp;exterior&nbsp;walls</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
  <span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;GetAllExteriorWalls(
  &nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;)&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">Wall</span>&gt;()
  &nbsp;&nbsp;&nbsp;&nbsp;.Where&lt;<span style="color:#2b91af;">Wall</span>&gt;(&nbsp;w&nbsp;=&gt;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IsExterior(&nbsp;w.WallType&nbsp;)&nbsp;);
  }
</pre>

Since the wall function filter is just checking a parameter value, the performance of this filtering process could be significantly enhanced by using
a [parameter filter](http://thebuildingcoder.typepad.com/blog/2010/06/parameter-filter.html) instead
of the slow .NET based `IsExterior` method post-processing.

Unfortunately, the wall function parameter is not always correctly set. In that case, of course, the GIGO principle applies:
[Garbage in, garbage out](https://en.wikipedia.org/wiki/Garbage_in,_garbage_out).

If you wish to avoid the dependency on the wall type and its parameters, you can try to judge whether a wall is exterior based on geometrical analysis instead.

The Revit API also provides a `BuildingEnvelopeAnalyzer` class that should help with this, but there seem to be problems using it, cf.:

- [Finding exterior walls by `BuildingEnvelopeAnalyzer`](https://forums.autodesk.com/t5/revit-api-forum/finding-exterior-walls-by-buildingenvelopeanalyzer/m-p/5647404)
- [Filtering exterior walls](https://forums.autodesk.com/t5/revit-api-forum/filtering-exterior-walls/m-p/5677706)

Some related challenges and solutions that might help here are discussed in The Building Coder topic group
on [2D Booleans and adjacent areas](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.2).

Yet another workaround was suggested: Place some room separation lines outside the building envelope and create a huge room around the entire building.
Then, it’s just a matter of getting room boundaries, filtering out the RSLs, appending the remaining elements to your list, deleting the room and RSLs, and moving up to the next level.
It may not work for some bad modelling cases, but catches most.

After further discussion with the development team, they asked:
Is the building model enclosed?
It needs to be in order for the analyzer to work.
In other words, do you have Roof and Floor elements to form enclosed spaces in the model?

####<a name="9"></a>Getting and Setting a Shared parameter Value

Let's end for today with a very basic question asked on StackOverflow,
on [how to get the value a room parameter defined by a third party](https://stackoverflow.com/questions/50285764/how-to-get-the-value-a-room-parameter-which-was-defined-by-a-third-party):

**Question:** How to get the value a room parameter, which was defined by a third party?

I am working on a shared project with many other people. One defined a custom parameter, which we must fill. I now want to fill that custom parameter with values from my database. But I can figure out, how to access this custom parameter which was created by a third party, since I don`t have a GUID...

Here is a screenshot of the parameter I want to change:

<center>
<img src="img/third_party_shared_parameter.png" alt="Third party shared parameter" width="260"/>
</center>

**Answer:** Reading and writing to a shared parameter is a very basic operation and is covered by
the [Revit API getting started material](http://thebuildingcoder.typepad.com/blog/about-the-author.html#2).
Please work through the video tutorials first of all, so you get an understanding of the basics.

After that, you can also take a look at
the [AdnRevitApiLabsXtra on GitHub](https://github.com/jeremytammik/AdnRevitApiLabsXtra).
It includes many examples of reading and writing parameters, both shared and built-in. 

Finally, the [FireRating and FireRatingCloud samples](https://github.com/jeremytammik/FireRatingCloud) both
demonstrate one single simple workflow that illustrates all aspects of what you need to know: 

- Create and bind a new shared parameter
- Export shared parameter values from Revit model to external database
- Import shared parameter values from external database to Revit model

