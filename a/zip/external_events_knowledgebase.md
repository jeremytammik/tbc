/a/doc/revit/tbc/doc/external_events_knowledgebase.txt

https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/Revit-API/files/GUID-0A0D656E-5C44-49E8-A891-6C29F88E35C0-htm.html

# External Events

The Revit API provides an External Events framework to accommodate the use of modeless dialogs. It is tailored for asynchronous processing and operates similarly to the Idling event with default frequency.

To implement a modeless dialog using the External Events framework, follow these steps:

- Implement an external event handler by deriving from the IExternalEventHandler interface
- Create an ExternalEvent using the static ExternalEvent.Create() method
- When an event occurs in the modeless dialog where a Revit action needs to be taken, call ExternalEvent.Raise()
- Revit will call the implementation of the IExternalEventHandler.Execute() method when there is an available Idling time cycle.

## IExternalEventHandler

This is the interface to be implemented for an external event. An instance of a class implementing this interface is registered with Revit, and every time the corresponding external event is raised, the Execute method of this interface is invoked.

The IExternalEventHandler has only two methods to implement, the Execute() method and GetName() which should return the name of the event. Below is a basic implementation which will display a TaskDialog when the event is raised.

Code Region: Implementing IExternalEventHandler

<pre>
public class ExternalEventExample : IExternalEventHandler
{
  public void Execute(UIApplication app)
  {
    TaskDialog.Show("External Event", "Click Close to close.");
  }

  public string GetName()
  {
    return "External Event Example";
  }
}
</pre>

## ExternalEvent

The ExternalEvent class is used to create an ExternalEvent. An instance of this class will be returned to an external event's owner upon the event's creation. The event's owner will use this instance to signal that the event should be called by Revit. Revit will periodically check if any of the events have been signaled (raised), and will execute all events that were raised by calling the Execute method on the events' respective handlers.

The following example shows the implementation of an IExternalApplication that has a method ShowForm() that is called from an ExternalCommand (shown at the end of the code region). The ShowForm() method creates a new instance of the external events handler from the example above, creates a new ExternalEvent and then displays the modeless dialog box which will later use the passed in ExternalEvent object to raise events.

Code Region: Create the ExternalEvent

<pre>
public class ExternalEventExampleApp : IExternalApplication
{
  // class instance
  public static ExternalEventExampleApp thisApp = null;
  // ModelessForm instance
  private ExternalEventExampleDialog m_MyForm;

  public Result OnShutdown(UIControlledApplication application)
  {
    if (m_MyForm != null && m_MyForm.Visible)
    {
      m_MyForm.Close();
    }

    return Result.Succeeded;
  }

  public Result OnStartup(UIControlledApplication application)
  {
    m_MyForm = null;   // no dialog needed yet; the command will bring it
    thisApp = this;  // static access to this application instance

    return Result.Succeeded;
  }

  //   The external command invokes this on the end-user's request
  public void ShowForm(UIApplication uiapp)
  {
    // If we do not have a dialog yet, create and show it
    if (m_MyForm == null || m_MyForm.IsDisposed)
    {
      // A new handler to handle request posting by the dialog
      ExternalEventExample handler = new ExternalEventExample();

      // External Event for the dialog to use (to post requests)
      ExternalEvent exEvent = ExternalEvent.Create(handler);

      // We give the objects to the new dialog;
      // The dialog becomes the owner responsible for disposing them, eventually.
      m_MyForm = new ExternalEventExampleDialog(exEvent, handler);
      m_MyForm.Show();
    }
  }
}

[Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
public class Command : IExternalCommand
{
  public virtual Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
  {
    try
    {
      ExternalEventExampleApp.thisApp.ShowForm(commandData.Application);
      return Result.Succeeded;
    }
    catch (Exception ex)
    {
      message = ex.Message;
      return Result.Failed;
    }
  }
}
</pre>

## Raise Event

Once the modeless dialog is displayed, the user may interact with it. Actions in the dialog may need to trigger some action in Revit. When this happens, the ExternalEvent.Raise() method is called. The following example is the code for a simple modeless dialog with two buttons: one to raise our event and one to close the dialog.

Code Region: Raise the Event

<pre>
public partial class ExternalEventExampleDialog : Form
{
  private ExternalEvent m_ExEvent;
  private ExternalEventExample m_Handler;

  public ExternalEventExampleDialog(ExternalEvent exEvent, ExternalEventExample handler)
  {
    InitializeComponent();
    m_ExEvent = exEvent;
    m_Handler = handler;
  }

  protected override void OnFormClosed(FormClosedEventArgs e)
  {
    // we own both the event and the handler
    // we should dispose it before we are closed
    m_ExEvent.Dispose();
    m_ExEvent = null;
    m_Handler = null;

    // do not forget to call the base class
    base.OnFormClosed(e);
  }

  private void closeButton_Click(object sender, EventArgs e)
  {
    Close();
  }

  private void showMessageButton_Click(object sender, EventArgs e)
  {
    m_ExEvent.Raise();
  }
}
</pre>

When the ExternalEvent.Raise() method is called, Revit will wait for an available Idling timecycle and then call the IExternalEventHandler.Execute() method. In this simple example, it will display a TaskDialog with the text "Click Close to close." as shown in the first code region above.

For a more complex example of using the External Events framework, see the sample code in the SDK under the ModelessDialog\ModelessForm_ExternalEvent folder. It uses a modeless dialog with numerous buttons and the IExternalEventHandler implementation has a public property to track which button was pressed so it can switch on that value in the Execute() method.
