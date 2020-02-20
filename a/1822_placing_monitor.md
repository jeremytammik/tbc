<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-api-forum/get-notified-when-a-family-type-is-about-to-place/m-p/9328378

twitter:

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

### Family Instance Placement Monitor

Yesterday, I mentioned a

Today, we discuss duplicating legend components in Python, my own non-API Python work and some undocumented utility methods:

- [UIFrameworkService utility methods](#4)

#### <a name="2"></a>

**Question:** 

<pre class="code">
</pre>

**Solution:**

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 900 -->
</center>

Many thanks to  for sharing this useful discovery.

#### <a name="3"></a>

#### <a name="4"></a>UIFrameworkService Utility Methods

While writing the above, I also conversed with Kennan Chen of Shanghai
on [getting notified when a family type is about to be placed](https://forums.autodesk.com/t5/revit-api-forum/get-notified-when-a-family-type-is-about-to-place/m-p/9327282).

He pointed out some interesting functionality that I was previously unaware of in UIFrameworkServices.dll:

**Question:** Is an event which can notify when Revit is about to place a family type?

There are events like `Application` `FamilyLoadedIntoDocument` and `FamilyLoadingIntoDocument`.

Is it possible to have another event `FamilyTypePlacingIntoDocument` for this?

Or is there a workaround?

**Answer:** As recently discussed, you
can [use the DocumentChanged event to detect the launching of a command](https://thebuildingcoder.typepad.com/blog/2020/01/torsion-tools-command-event-and-info-in-da4r.html#3).

**Response:** It works great to catch the placing FamilyType event triggered by placing type directly from Revit UI.

The code I use:

<pre class="code">
</pre>

Wow, how exciting to see my post in your blog. Lucky day 🙂

After hours of struggle, I finally end this up by simply using a Timer to constantly check the currently placing type until the UI refreshes and the API call returns correctly. It may not be the best solution, but at least it works.

I wrapped the logic up in the following class to make it easier. Hope this can be helpful. Pay more attention to the potential multi-thread risk and UI performance impact if someone wants to use the code.

jeremytammik says:

Thank you for the appreciation. My pleasure entirely.

Wow, your FamilyPlacingMonitorService looks exciting.

I know little about the dispatcher and that stuff, so I hardly understand at all what you are doing.

Would you mid explaining step by step how this service works, for others not so experienced in this kind of stuff?

I would love to share this as well. I think many developers can learn a lot from it.

Thank you!

303353762 says:

My pleasure to share this to people who are in need.

As a Chinese, I'm not quite good at explaining things in English. I'll try my best to be accurate. 😛

I'll ignore the part how to get notified about a family symbol placing event.

Once the placing event is caught, the next job is to get the placing family symbol.

The core method is UIFrameworkServices.TypeSelectorService.getCurrentTypeId() from UIFrameworkServices.dll. This DLL ships with Revit, and can be easily found in Revit root folder.

I believe this method is designed for Revit UI framework to get data for the Properties panel, since every time this method is invoked, the returned value is always the id of the ElementType that is currently displayed in the Properties panel.

The biggest problem is, when a family symbol is to be placed, by API call or by UI click, this method doesn't return correctly. I guess the state doesn't change before Revit really enters placing mode. But when Revit enters placing mode, no code can be ran since the command loop in Revit is stuck. That's frustrating!

Luckily, another loop is still running, the message loop in every windows UI application running in STA mode. In WPF, the message loop is started by Dispatcher in the main UI thread. UI updates must be queued by dispatcher to be executed in the main UI thread synchronously. That's exactly the same mechanism adopted by Revit known as ExternalEvent. In the FamilyPlacingMonitorService class, method DispatcherInvoke tests whether the execution is currently in main UI thread. If not, it queues the delegate method to the UI thread.

To run code after Revit entering placing mode, a Timer is created ahead of time to constantly try to resolve the currently placing symbol. But the Timer doesn't run code in the UI thread, which means it's not safe for Revit API to run. The trick is to queue the Timer callback to the main UI thread using Dispatcher.

Every UI object (DispatcherObject specifically) holds a reference to the Dispatcher instance, it's easy to get that instance from the Ribbon object (Autodesk.Windows.ComponentManager.Ribbon).

I added some comments trying to explain the code clearly

public class FamilyPlacingMonitorService
    {
        #region Constructors

        public FamilyPlacingMonitorService(Application app)
        {
            app.DocumentChanged += App_DocumentChanged;
        }

        #endregion

        #region Others

        public event EventHandler<FamilySymbol> FamilySymbolPlacingIntoDocument;

        private void App_DocumentChanged(object sender, DocumentChangedEventArgs e)
        {
            var transactionName = e.GetTransactionNames().FirstOrDefault();
            if (transactionName == "Modify element attributes")
            {
                //treat transactions with the name "Modify element attributes" as element placing
                //maybe not accurate, but enough to cover most scenes
                var document = e.GetDocument();

                //try to get the current placing family symbol
                if (ResolveCurrentlyPlacingFamilySymbol(document) is FamilySymbol familySymbol)
                {
                    //got ya, notify via event
                    OnFamilySymbolPlacingIntoDocument(familySymbol);
                }
                else
                {
                    //current type doesn't refreshed, create a Timer to constantly try
                    Timer timer = null;
                    //only queue one resolving logic to the main thread
                    var checking = false;
                    timer = new Timer(s =>
                    {
                        if (!checking)
                        {
                            checking = true;
                            //try to queue the resolving logic to main thread to avoid multi-thread risk
                            DispatcherInvoke(() =>
                            {
                                if (ResolveCurrentlyPlacingFamilySymbol(document) is FamilySymbol symbol)
                                {
                                    //got ya, notify via event
                                    OnFamilySymbolPlacingIntoDocument(symbol);
                                    //release the timer
                                    timer?.Change(0, Timeout.Infinite);
                                    timer?.Dispose();
                                }
                                else
                                {
                                    checking = false;
                                }
                            });
                        }
                    }, null, 0, 100);
                }
            }
        }

        private void DispatcherInvoke(Action action)
        {
            if (ComponentManager.Ribbon.Dispatcher?.CheckAccess() ?? false)
            {
                //currently on main thread, execute directly
                action?.Invoke();
            }
            else
            {
                //not on main thread, queue the delegate to man ui thread
                ComponentManager.Ribbon.Dispatcher?.Invoke(action);
            }
        }

        private void OnFamilySymbolPlacingIntoDocument(FamilySymbol symbol)
        {
            FamilySymbolPlacingIntoDocument?.Invoke(this, symbol);
        }

        private FamilySymbol ResolveCurrentlyPlacingFamilySymbol(Document document)
        {
            var id = TypeSelectorService.getCurrentTypeId();
            if (id > 0)
            {
                if (document.GetElement(new ElementId(id)) is FamilySymbol symbol)
                {
                    return symbol;
                }
            }

            return null;
        }

        #endregion
    }
