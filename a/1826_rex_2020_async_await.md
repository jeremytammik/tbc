<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- rex sdk
  /a/doc/revit/tbc/git/a/zip/
  A REX problem appeared in Revit 2019 and was resolved by sharing a custom version of the REX SDK.
  Now the same problem occurs again with Revit 2020:
  https://forums.autodesk.com/t5/revit-api-forum/issue-with-sdk-and-visual-studio/td-p/8052988
  Resolution for Revit 2019:
  https://thebuildingcoder.typepad.com/blog/2018/06/rex-sdk-visual-studio-templates-for-revit-structure-2019.html#2
  REX SDK2020.ZIP
  Structural Analysis SDK2020.zip

- https://thebuildingcoder.typepad.com/blog/2020/02/external-communication-and-async-await-event-wrapper.html#comment-4813648335

Kennan Chen wrote:

What a coincidence.
I also implemented my own async/await external event wrapper these days.
After some comparison with the one WhiteSharq provided, I found my implementation contains more useful functionality including wrapping async delegates and exposing core ability to define enhanced external events.
It internally adopts an `ExternalEvent` creator, so the developers won't accidentally experience the context problems.
Hope it can be helpful to the community.
Github:

https://github.com/KennanChan/Revit.Async

Also available via nuget.

/a/src/rvt/Revit.Async/

- headless revit
https://forums.autodesk.com/t5/revit-api-forum/family-related-memory-leaks/m-p/8738515

Kennan.Chen in reply to: Explorergeorge.aggreySNQ7D
‎2020-03-02 02:16 PM 
Re: Family related memory leaks 
A possible alternative is to run Revit in headless mode. Totally contrary to the documented approach to create an add-in, you can start an application which hosts a Revit runtime within the same process which enables you to do what you want with the top-level Application object. Just like Navisworks.

In your case, for each project, start a headless Revit to finish your process, then close the application.

The problem is, for each project, there is a waste of boot time.

Since headless Revit don't start the renderer and anything about the UI, less memory will be consumed to make it possible to run serveral tasks in parallel on your machine. Moreover you can even set up a cluster to handle tons of projects in parallel which I believe is the key to enable Forge to resolve Revit files in cloud.

As to how to set up a headless Revit, find a file named lcldrevit.dll or lcrvtutil.dll(newer version of Navisworks) under {Navisworks root folder}\Loaders\Rx folder. By decompiling that file,  LcRevitLoad.DoInit() contains all you need to start your own headless Revit.

To make things easy enough, I created a library called Revit.Headless to do all that loading logic for you. Visit https://github.com/KennanChan/Revit.Headless for more details. Also it's available via nuget.

https://github.com/KennanChan/Revit.Headless

twitter:

 the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="100"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Another Async Await, REX and Structural Analysis SDK

#### <a name="2"></a> REX SDK and Structural Analysis SDK 2020

Last year,
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on an [issue with SDK and Visual Studio](https://forums.autodesk.com/t5/revit-api-forum/issue-with-sdk-and-visual-studio/td-p/8052988)
poinbted out that the Visual Studio templates provided with the REX SDK to make use of the Revit Structure Extensions had not yet been updated for Revit 2019.

That was fixed by the development team and
an [updated set of REX SDK Visual Studio templates for Revit Structure 2019](https://thebuildingcoder.typepad.com/blog/2018/06/rex-sdk-visual-studio-templates-for-revit-structure-2019.html#2) was
shared in the forum and here on the blog.

The same situation arose again with Revit 2020, and here are the updated templates, samples and documentation for Revit 2020:

- [REX SDK2020.ZIP](zip/REX SDK2020.ZIP)
- [Structural Analysis SDK2020.zip](Structural Analysis SDK2020.zip)

**Response:** The REX SDK installed and is now working!

I read all provided PDF documentation and did the Hello World exercise.
I have some basic questions on how to get started with my specific projects and will create a separate post for that when I'm ready. 

I haven't looked at the Structural Analysis SDK as we are going to link Revit with our own custom proprietary software.
After reading about element mapping between databases and the content generator, I'm relieved to know that the REX SDK will likely reduce a lot of development time since those functionalities already exist. 

Thank you for providing the updates! I'm glad to have begun my journey down this path.


#### <a name="3"></a> Revit.Async

Last month, we briefly looked at a
simple [`async` and `await` external event wrapper](https://thebuildingcoder.typepad.com/blog/2020/02/external-communication-and-async-await-event-wrapper.html#2).

Kennan Chen kindly reacted to that post in a [comment](https://thebuildingcoder.typepad.com/blog/2020/02/external-communication-and-async-await-event-wrapper.html#comment-4813648335),
saying:

> What a coincidence.
I also implemented my own async/await external event wrapper these days.
After some comparison with the one WhiteSharq provided, I found my implementation contains more useful functionality including wrapping async delegates and exposing core ability to define enhanced external events.
It internally adopts an `ExternalEvent` creator, so the developers won't accidentally experience the context problems.
Hope it can be helpful to the community.
[Revit.Async Github repositroy](https://github.com/KennanChan/Revit.Async) showing
how to use the task-based asynchronous pattern (TAP) to run Revit API code from any execution context.
Also available via nuget.

Kennan's implementation does indeed look very complete and impressive with extensive documentation in both English
and [Chinese](https://github.com/KennanChan/Revit.Async/blob/master/%E8%AF%B4%E6%98%8E.md):

#### <a name="3.2"></a> Revit.Async Background

If you have ever encountered a Revit API exception saying "Cannot execute Revit API outside of Revit API context",
typically when you want to execute Revit API code from a modeless window, you may need this library to save your life.

A common solution for this exception is to wrap the Revit API code using `IExternalEventHandler` and register the handler instance to Revit ahead of time to get a trigger (`ExternalEvent`).
To execute the handler, just raise the trigger from anywhere to queue the handler to the Revit command loop.
But there comes another problem.
After raising the trigger, within the same context, you have no idea when the handler will be executed and it's not easy to get some result generated from that handler.
If you do want to make this happen, you have to manually yield the control back to the calling context.

This solution looks quite similar to the mechanism of "Promise" if you are familiar with JavaScript ES6.
Actually we can achieve all the above logic by making use of task-based asynchronous pattern (TAP) which is generally known as `Task<T>` in .NET.
By adopting Revit.Async, it is possible to run Revit API code from any context, because internally Revit.Async wraps your code automatically with `IExternalEventHandler` and yields the return value to the calling context to make your invocation more natural.

If you are unfamiliar with the task-based asynchronous pattern (TAP), here is some useful material on it provided by Microsoft:

- [Task-based asynchronous pattern (TAP)
](https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap)
- [Task asynchronous programming model](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/task-asynchronous-programming-model)

Here is a [diagram comparing the Revit API external event mechnism with Revit.Async](https://drive.google.com/file/d/1sb6Yrlt6zjkE9XBh4UB5sWV_i8nTpkmG/view?usp=sharing) and screenshots of the two main parts:

<center>
<img src="img/revit_async_external_event.png" alt="Revit API external event" title="Revit API external event" width="100"/>
<p style="font-size: 80%; font-style:italic">Revit API external event</p>

<img src="img/revit_async_external_event.png" alt="Revit.Async" title="Revit.Async" width="100"/>
<p style="font-size: 80%; font-style:italic">Revit.Async</p>
</center>

#### <a name="3.3"></a> Example &ndash; Standard Approach without Revit.Async

<pre class="code">
[Transaction(TransactionMode.Manual)]
public class MyRevitCommand : IExternalCommand
{
    public static ExternalEvent SomeEvent { get; set; }
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        //Register MyExternalEventHandler ahead of time
        SomeEvent = ExternalEvent.Create(new MyExternalEventHandler());
        var window = new MyWindow();
        //Show modeless window
        window.Show();
        return Result.Succeeded;
    }
}

public class MyExternalEventHandler : IExternalEventHandler
{
    public void Execute(UIApplication app)
    {
        //Running some Revit API code here to handle the button click
        //It's complicated to accept argument from the calling context and return value to the calling context
        var families = new FilteredElementCollector(app.ActiveUIDocument.Document)
                            .OfType(typeof(Family))
                            .ToList();
        //ignore some code
    }
}

public class MyWindow : Window
{
    public MyWindow()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        Width                 = 200;
        Height                = 100;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        var button = new Button
        {
            Content             = "Button",
            Command             = new ButtonCommand(),
            VerticalAlignment   = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        Content = button;
    }
}

public class ButtonCommand : ICommand
{    
    public bool CanExecute(object parameter)
    {
        return true;
    }

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter)
    {
        //Running Revit API code directly here will result in a "Running Revit API outside of Revit API context" exception
        //Raise a predefined ExternalEvent instead
        MyRevitCommand.SomeEvent.Raise();
    }
}
</pre>

#### <a name="3.4"></a> Example &ndash; Revit.Async Approach

<pre class="code">
[Transaction(TransactionMode.Manual)]
public class MyRevitCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        //Always initialize RevitTask ahead of time within Revit API context
        RevitTask.Initialze();
        var window = new MyWindow();
        //Show modeless window
        window.Show();
        return Result.Succeeded;
    }
}

public class MyWindow : Window
{
    public MyWindow()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        Width                 = 200;
        Height                = 100;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        var button = new Button
        {
            Content             = "Button",
            Command             = new ButtonCommand(),
            CommandParameter    = true,
            VerticalAlignment   = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        Content = button;
    }
}

public class ButtonCommand : ICommand
{    
    public bool CanExecute(object parameter)
    {
        return true;
    }

    public event EventHandler CanExecuteChanged;

    public async void Execute(object parameter)
    {
        //.NET 4.5 supported keyword, use ContinueWith if using .NET 4.0
        var families = await RevitTask.RunAsync(
            app => 
            {
                //Run Revit API code here
                
                //Taking advantage of the closure created by the lambda expression,
                //we can make use of the argument passed into the Execute method.
                //Let's assume it's a boolean indicating whether to filter families that is editable
                if(parameter is bool editable)
                {
                    return new FilteredElementCollector(app.ActiveUIDocument.Document)
                        .OfType(typeof(Family))
                        .Cast<Family>()
                        .Where(family => editable ? family.IsEditable : true)
                        .ToList();
                }
                
                return null;
            });
        
        MessageBox.Show($"Family count: {families?.Count ?? 0}");
    }
}
</pre>

## Define your own handler

Fed up with the weak `IExternalEventHandler` interface?
Use the `IGenericExternalEventHandler<TParameter,TResult>` interface instead.
It provides you with the ability to pass argument to a handler and receive result on complete.

It's always recommended to derive from the predefined abstract classes; they are designed to handle the argument passing and result returning part.

| Class                                                   | Description                       |
| ------------------------------------------------------- | --------------------------------- |
| `AsyncGenericExternalEventHandler<TParameter, TResult>` | Use to execute asynchronous logic |
| `SyncGenericExternalEventHandler<TParameter, TResult>`  | Use to execute synchronize logic  |

<pre class="code">
[Transaction(TransactionMode.Manual)]
public class MyRevitCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        RevitTask.Initialize();
        //Register SaveFamilyToDesktopExternalEventHandler ahead of time
        RevitTask.RegisterGlobal(new SaveFamilyToDesktopExternalEventHandler());
        var window = new MyWindow();
        //Show modeless window
        window.Show();
        return Result.Succeeded;
    }
}

public class MyWindow : Window
{
    public MyWindow()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        Width                 = 200;
        Height                = 100;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        var button = new Button
        {
            Content             = "Save Random Family",
            Command             = new ButtonCommand(),
            CommandParameter    = true,
            VerticalAlignment   = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        Content = button;
    }
}

public class ButtonCommand : ICommand
{    
    public bool CanExecute(object parameter)
    {
        return true;
    }

    public event EventHandler CanExecuteChanged;

    public async void Execute(object parameter)
    {
        var savePath = await RevitTask.RunAsync(
            async app =>
            {
                try
                {
                    var document = app.ActiveUIDocument.Document;
                    var randomFamily = await RevitTask.RunAsync(
                        () =>
                        {
                            var families = new FilteredElementCollector(document)
                                .OfClass(typeof(Family))
                                .Cast<Family>()
                                .Where(family => family.IsEditable)
                                .ToArray();
                            var random = new Random(Environment.TickCount);
                            return families[random.Next(0, families.Length)];
                        });

                    //Raise your own handler
                    return await RevitTask.RaiseGlobal<SaveFamilyToDesktopExternalEventHandler, Family, string>(randomFamily);
                }
                catch (Exception)
                {
                    return null;
                }
            });
        var saveResult = !string.IsNullOrWhiteSpace(savePath);
        MessageBox.Show($"Family {(saveResult ? "" : "not ")}saved:\n{savePath}");
        if (saveResult)
        {
            Process.Start(Path.GetDirectoryName(savePath));
        }
    }
}

public class SaveFamilyToDesktopExternalEventHandler : 			
	SyncGenericExternalEventHandler<Family, string>
{
    public override string GetName()
    {
        return "SaveFamilyToDesktopExternalEventHandler";
    }

    protected override string Handle(UIApplication app, Family parameter)
    {
        //write sync logic here
        var document       = parameter.Document;
        var familyDocument = document.EditFamily(parameter);
        var desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        var path = Path.Combine(desktop, $"{parameter.Name}.rfa");
        familyDocument.SaveAs(path, new SaveAsOptions {OverwriteExistingFile = true});
        return path;
    }
}
</pre>

Impressive, isn't it?

Very many thanks to Kennan for this great implementation and explanation!
