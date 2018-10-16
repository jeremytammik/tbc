# How to convert your addin to work with Design Automation for Revit

Thank you so much for your interest in Design Automation for Revit. 

## Start with a small subset of your code

You'll want to start with a single operation. Converting an External Command may be a good idea. 

## Referencing the DesignAutomationBridge DLL

The `DesignAutomationBridge.dll` is only available to participant in the private beta.


## Convert your IExternalApplication or IExternalCommand to IExternalDBApplication

You won't be adding any buttons or ribbon commands, since there won't be any UI interaction. 

You will need to implement `OnStartup` and `OnShutdown`. These functions will get a `ControlledApplication` instead of a `UIControlledApplication`. The functions return an `ExternalDBApplicationResult` object. 

<pre>
  using Autodesk.Revit.ApplicationServices;
  using Autodesk.Revit.DB;
  using DesignAutomationFramework;
  namespace DeleteWalls
  {   
    [Transaction(TransactionMode.Manual)]
    public class DeleteWallsApp : IExternalDBApplication
    {
      public ExternalDBApplicationResult OnStartup(
        ControlledApplication app)
      {
        return ExternalDBApplicationResult.Succeeded;
      }
  
      public ExternalDBApplicationResult OnShutdown(
        ControlledApplication app)
      {
        return ExternalDBApplicationResult.Succeeded;
      }
</pre>

The .addin file can go in the normal place, but the addin type is `DBApplication`.

* Don't include references to RevitAPIUI! (Don't include WPF or Windows Forms or anything either, but we do not currently have a way to check this.) There's no UI interaction, so anything that pops up a dialog expecting user input will hang the system. 

## Add a reference to DesignAutomationBridge.dll and add an event handler for DesignAutomationReady

Add a reference `DesignAutomationBridge.dll`.  

> For a C# project in Visual Studio, this is done by opening the Solution Explorer, finding your C# project, expanding its contents, right-clicking on the References node and doing 'Add Reference...'  
> In the Reference Manager dialog, use the 'Browse...' button to browse to DesignAutomationBridge.dll.  Click 'Add' and then 'OK' to add the reference to your project.

The `DesignAutomationBridge` defines an event `DesignAutomationReadyEvent`. Revit's engine will raise this event when it's ready for you to run your addin. You should execute your code inside the event handler. 

<pre>
  public class DeleteWallsApp : IExternalDBApplication
  {
    public ExternalDBApplicationResult OnStartup(
      ControlledApplication app)
    {
      DesignAutomationBridge.DesignAutomationReadyEvent
       += HandleDesignAutomationReadyEvent;
      return ExternalDBApplicationResult.Succeeded;
    } 
    public void HandleDesignAutomationReadyEvent(
      object sender,
      DesignAutomationReadyEventArgs e)
    {
      e.Succeeded = true;
      DeleteAllWalls(e.DesignAutomationData);
    }
</pre>

The event will give you a path `DesignAutomationData.MainModelPath` to the "main" model indicated in the WorkItem's arguments. (We do not pre-open this model for you.) There is also a success/failure argument `DesignAutomationReadyEventArgs.Succeeded` you should set; it will let the service know whether potential failures happened in your code or elsewhere. 

Any files you load or create should be put into the working directory. On the cloud your write access is limited to the working directory and its children.
 
## Handle failures encountered by Revit

A fundamental feature in Revit is how warnings and errors (collectively referred to as "failures") are handled.  Understand your options for [handling failures](FailureProcessor.html) in Revit and implement a failure handling strategy in your application.

## Check that it works locally

We are currently working on an application which will make testing more convenient. However, we have provided an alternate way of testing:

#### Handling Revit's `ApplicationInitialized` Event

Don't use this event on the cloud, because Design Automation for Revit continues doing setup past the point at which `ApplicationInitialized` is raised. Locally it should mimic the "run automatically" behavior. For example, in `DeleteWalls`, we can do this:

<pre>
  public class DeleteWallsApp : IExternalDBApplication
  {
    public ExternalDBApplicationResult OnStartup(Autodesk.Revit.ApplicationServices.ControlledApplication app)
    {
      //Stop handling the event used by jobs on the cloud:
      //DesignAutomationBridge.DesignAutomationReadyEvent += HandleDesignAutomationReadyEvent;
      // And instead execute the code when desktop Revit is initialized.
      app.ApplicationInitialized += HandleApplicationInitializedEvent;
      return ExternalDBApplicationResult.Succeeded;
    }
    
    //public void HandleDesignAutomationReadyEvent(object sender, DesignAutomationReadyEventArgs e)
    //{
    //  e.Succeeded = true;
    //  DeleteAllWalls(e.DesignAutomationData);
    //}
    
    public void HandleApplicationInitializedEvent(object sender, Autodesk.Revit.DB.Events.ApplicationInitializedEventArgs e)
    {
      Autodesk.Revit.ApplicationServices.Application app = sender as Autodesk.Revit.ApplicationServices.Application;
      // We don't need to provide the file
      DesignAutomationData data = new DesignAutomationData(app, "/path/to/file.rvt");
      DeleteAllWalls(data);
    }
</pre>

Additionally, we must provide the .addin file to Revit. Add the add-in manifest to the appropriate location, e.g., *C:/ProgramData/Autodesk/Revit/Addins/2019* and set the `<Assembly>` tag to point to your DLL:

<pre>
  &lt;Assembly&gt;C:/test/DeleteWalls/DeleteWallsTest/bin/Debug/DeleteWalls.dll&lt;/Assembly&gt;
</pre>

This way, we are able to run this locally without any UI intervention on Revit startup. See [this guide](http://usa.autodesk.com/adsk/servlet/index?siteID=123112&id=20132893) on debugging.

**Note:** Your application cannot use the network, or write to any files outside of the current working directory. Restrictions on Design Automation for Revit can be found [here](QuotasAndRestrictions.html).

