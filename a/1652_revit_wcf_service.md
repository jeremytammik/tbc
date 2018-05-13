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

 #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon 

&ndash; 
...

--->

### 

####<a name="2"></a>Driving Revit from a Modeless Context via a WCF Service

Over the years, we explored numerous different ways
to [drive Revit from outside, from a modelesss context, via the `Idling` and external events](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28).

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

Unfortunately, it stopped working in the Revit 2018 timeframe.

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
  TaskContainer.Instance.EnqueueTask(GetDocumentPath); // <- enque GetDocumentPath task
 
  Monitor.Wait(Locker, WaitTimeout);// <- waiting for invoking Monitor.Pulse(Locker) somewhere in desired time interval
}
 
return currentDocumentPath;
</pre>

The calling of Monitor.Pulse in task processing method:
 
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
      Monitor.Pulse(Locker); //<- HERE!
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
        var serviceHost = new ServiceHost(typeof(RevitExternalService), new Uri(ServiceUrlHttp), new Uri(ServiceUrlTcp));
 
        serviceHost.Description.Behaviors.Add(new ServiceMetadataBehavior());
        serviceHost.AddServiceEndpoint(typeof(IRevitExternalService), new BasicHttpBinding(), ServiceUrlHttp);
        serviceHost.AddServiceEndpoint(typeof(IRevitExternalService), new NetTcpBinding(), ServiceUrlTcp);
        serviceHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
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

With that, it started to work again.

I use `http` binding, so Revit should be started as administrator, or you should use
the [`ServiceModel` registration tool](https://docs.microsoft.com/en-us/dotnet/framework/wcf/servicemodelreg-exe) to register WCF service for specific application.
 
Look at my forked repository: https://github.com/CADBIMDeveloper/RevitExternalAccessDemo
 
I also was interested, why it stopped working. Was it due to Revit mechanism change or due to changes in .Net 4.7 framework? I created simple application, that started WCF service, but real request processing is in separate thread (I use the same TaskContainer):
 
public partial class Form1 : Form
{
  private const string ServiceUrlHttp = "http://localhost:9001/ExternalService";
  private const string ServiceUrlTcp = "net.tcp://localhost:9002/ExternalService";
 
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
 
    var serviceHost = new ServiceHost(typeof(ExternalService), new Uri(ServiceUrlHttp), new Uri(ServiceUrlTcp));
 
    serviceHost.Description.Behaviors.Add(new ServiceMetadataBehavior());
    serviceHost.AddServiceEndpoint(typeof(IExternalService), new BasicHttpBinding(), ServiceUrlHttp);
    serviceHost.AddServiceEndpoint(typeof(IExternalService), new NetTcpBinding(), ServiceUrlTcp);
    serviceHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
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
 
It works as expected, so this change is, probably, side effect of some changes in Revit itself

I forked Victor's repository and make this thing to work in Revit 2019.



####<a name="3"></a>Room Walls' Directions

- https://forums.autodesk.com/t5/revit-api-forum/room-walls-direction/m-p/7995914
  room wall directions
  two solution by beinoit to
  inside the room versus outside
  angle to north

Q: How to get the direction of the walls of a room. I mean the  interior side of wall is facing in what direction. East West North South  Northeast   Northwest  Southeast  Southwest .

Assume I am in the room and I  am looking at the wall. I need to get this direction.

A:

Look at the DirectionCalculation SDK sample and this The Building Coder discussion:

http://thebuildingcoder.typepad.com/blog/2010/01/south-facing-walls.html

Benoit suggests using the most simple solution : draw a semi-infinite line from the point you are checking to a point of the face of the wall, and check the number of times it crosses the walls of the room. If you cross them an even number, you are inside the room. Elseway, you are outside.

If it is the direction you need, draw a line between your position and a point of the face of the wall. Then compute the angle to the North. You have your direction.


####<a name="4"></a>Getting and Setting a Shared parameter Value

- https://stackoverflow.com/questions/50285764/how-to-get-the-value-a-room-parameter-which-was-defined-by-a-third-party

How to get the value a room parameter, which was defined by a third party

I am working on a shared project with many other people. One defined a custom parameter, which we must fill. I now want to fill that custom parameter with values from my database. But I can figure out, how to access this custom parameter which was created by a third party, since I don`t have a GUID...

Here is a screenshot of the parameter I want to change:

img/third_party_shared_parameter.png

Reading and writing to a shared parameter is a very basic operation and is covered by the [Revit API getting started material](http://thebuildingcoder.typepad.com/blog/about-the-author.html#2). Please work through the video tutorials first of all, so you get an understanding of the basics.

After that, you can also take a look at the [AdnRevitApiLabsXtra on GitHub](https://github.com/jeremytammik/AdnRevitApiLabsXtra). It includes many examples of reading and writing parameters, both shared and built-in. 

Finally, the [FireRating and FireRatingCloud samples](https://github.com/jeremytammik/FireRatingCloud) both demonstrate one single simple thing: 

- Create and bind a new shared parameter
- Export shared parameter values from Revit model to external database
- Import shared parameter values from external database to Revit model


<pre class="code">
</pre>



<center>
<img src="img/.png" alt="" width=""/>
</center>
