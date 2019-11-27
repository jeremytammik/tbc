<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---


twitter:

- post the news about Design Automation webinars
  https://forge.autodesk.com/blog/design-automation-api-webinar-series-register-now
  /a/doc/revit/tbc/git/a/img/da_for_air3.png
 
- Nice sample showing two stacked ribbon items versus 3:
  24x24 StackedItems
  https://forums.autodesk.com/t5/revit-api-forum/24x24-stackeditems/m-p/9168470

- Very important hint from Fair59 on reinitialising the filtered element collector
  https://forums.autodesk.com/t5/revit-api-forum/collection-of-elements-created-using-elementworksetfilter-giving/m-p/9164018
  This misunderstanding caused a similar problem in another recent case...

 the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon 

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Design Automation Webinars

If you are looking for the ultimatively painless introduction to the Forge Design Automation API, you are in luck: the Desi9gn Automation webinars are coming up.

If you are more interested in the desktop Revit API, the solution for creating two stacked ribbon items instead of three might be more to your tase.

In either case, teh tip on reinitiklising a filtered element collector before reusing it is importantissimo in both contexts:



<center>
<img src="img/da_for_air3.png" alt="Forge Design Automation API for AutoCAD, Inventor, Revit and 3DS Max" width="400"> <!--800-->
</center>

<pre class="code">
</pre>


####<a name="2"></a> Forge Desing Automation Webinars Coming Up

A new series of webinars on
the [Forge Design Automation APIs for AutoCAD, Inventor, Revit and 3DS Max](https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/) is
coming up.

You can register now to participate and also to gain access to the recordings that will be posted after the live events.

There is still time to get hands on with the new Design Automation APIs.
Four webinars are scheduled in December that will cover each of the Design Automation APIs in depth.
The recordings will be posted after so be sure to register to take advantage.

All webinars begin at 8:00 AM Pacific Standard Time.

- AutoCAD &ndash; December 5
<br/>Design Automation API for AutoCAD on Forge
<br/>Albert Szilvasy, software Architect will share details about the new and updated API.
<br/>This webinar discusses how the Design Automation for AutoCAD empowers customers and partners to get more work done more quickly, reliably and collaboratively using cloud-based web services.
We will demonstrate the ability to run scripts on your design files, leveraging the scale of the Forge Platform to automate repetitive tasks.
<br/>[Register](https://autodesk.zoom.us/webinar/register/WN_n-yZWaSNSW-OIMJRDdJBZw)
- Revit &ndash; December 10
<br/>Design Automation API for REVIT on Forge
<br/>Sasha Crotty, Senior Product Manager, Revit Platform & Services to share updates on this API.
<br/>This webinar discusses how the Design Automation for Revit empowers customers and partners to get more work done more quickly, reliably and collaboratively using cloud-based web services.
We will demonstrate how you can automate your most common, manual, and error-prone work to improve responsiveness and free up your time so you can focus on more valuable work.
<br/>[Register](https://autodesk.zoom.us/webinar/register/WN_50PU3thnSfC8m-Rh2PE2Ag)
- Inventor &ndash; December 11
<br/>Design Automation API for Inventor on Forge
<br/>Andrew Akenson, software Architect will shares details about the new and updated APIs added to the Design Automation for Inventor on Forge.
<br/>This webinar discusses how the Design Automation for Inventor empowers customers and partners to get more work done more quickly, reliably and collaboratively using cloud-based web services.
We will demonstrate how you can automate your most common, manual, and error-prone work to improve responsiveness and free up your time so you can focus on more valuable work.
<br/>[Register](https://autodesk.zoom.us/webinar/register/WN_8poFofy4QWCfq0ciL0AYjg)
- 3DS Max &ndash; December 12
<br/>Design Automation API for 3ds Max on Forge
<br/>Kevin Vandecar, Developer Advocate on the Forge Partner Development team will share details about the newly launched Design Automation API for 3ds Max on Forge.
<br/>In this webinar, we will discuss how the Design Automation for 3ds Max empowers customers and partners to get more work done more quickly, reliably and collaboratively using cloud-based web services.
Using automation routines, you can build custom solutions using 3ds Max in the cloud.
No local resources are needed, so it could be a commercial website/configurator type webapp, or it could be a pipeline automation that run from your in-house tools.
The sky (or rather the cloud) is the limit.
We will demonstrate how you can easily automate common workflows in 3ds Max.
<br/>[Register](https://autodesk.zoom.us/webinar/register/WN_7jTFtqz3Tte76LswrUACvw)


####<a name="3"></a> Stacking Two 24x24 Ribbon Items 

Jameson Nyp, BIM Manager and IS Director at [Telios Engineering](https://teliospc.com) in Dallas, Texas, shares a nice solution for stacking two ribbon items in
his [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [24x24 StackedItems](https://forums.autodesk.com/t5/revit-api-forum/24x24-stackeditems/m-p/9168470):

**Qutestion:** This may be an easy one, but so far I am struggling to find anything specific about it.

How do you make a `StackedItem` where the icons are 24x24 when there are only 2 in the stack?

It seems like it should be possible, as it is used multiple times in the modify tab:

<center>
<img src="img/modify_tab_icon_sizes.png" alt="Modify tab icon sizes" width="400"> <!--800-->
</center>

I have been able to set the ShowText property to false to get the 3 stacked icons, but when I use the same methodology with the 2 icon stack it remains 16x16 regardless of the icon resolution.

I have tried to obtain and change the button's height and width, minWidth and minHeight through the Autodesk.Window.RibbonItem object to no avail.

Has anyone had any success in creating these icons?

**Answer:** I found a solution.

In order to display the button at the 24x24 size, the Autodesk.Windows.RibbonItem.Size needs to be manually set to Autodesk.Windows.RibbonItemSize.Large enum and a 24x24 icon needs to be set to the button's `LargeImage` property.

Here is a code samnple:

<pre class="code">
using Autodesk.Revit.UI;
using Autodesk.Windows;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using YourCustomUtilityLibrary;
 
namespace ReallyCoolAddin
{
  public class StackedButton
  {
    public IList<Autodesk.Revit.RibbonItem> Create(RibbonPanel ribbonPanel)
    {
      // Get Assembly
      Assembly assembly = Assembly.GetExecutingAssembly();
      string assemblyLocation = assembly.Location;
 
      // Get DLL Location
      string executableLocation = Path.GetDirectoryName(assemblyLocation);
      string dllLocationTest = Path.Combine(executableLocation, "TestDLLName.dll");
 
      // Set Image
      BitmapSource pb1Image = UTILImage.GetEmbeddedImage(assembly, "Resources.16x16_Button1.ico");
      BitmapSource pb2Image = UTILImage.GetEmbeddedImage(assembly, "Resources.16x16_Button2.ico");
      BitmapSource pb1LargeImage = UTILImage.GetEmbeddedImage(assembly, "Resources.24x24_Button1.ico");
      BitmapSource pb2LargeImage = UTILImage.GetEmbeddedImage(assembly, "Resources.24x24_Button2.ico");
 
      // Set Button Name
      string buttonName1 = "ButtonTest1";
      string buttonName2 = "ButtonTest2";
 
      // Create push buttons
      PushButtonData buttondata1 = new PushButtonData(buttonName1, buttonTextTest, dllLocationTest, "Command1");
      buttondata1.Image = pb1Image;
      buttondata1.LargeImage = pb1LargeImage;
 
      PushButtonData buttondata2 = new PushButtonData(buttonName2, buttonTextTest, dllLocationTest, "Command2");
      buttondata2.Image = pb2Image;
      buttondata2.LargeImage = pb2LargeImage;
 
      // Create StackedItem
      IList<Autodesk.Revit.RibbonItem> ribbonItem = ribbonPanel.AddStackedItems(buttondata1, buttondata2);
 
      // Find Autodes.Windows.RibbonItems
      UTILRibbonItem utilRibbon = new UTILRibbonItem();
      var btnTest1 = utilRibbon.getButton("Tab", "Panel", buttonName1);
      var btnTest2 = utilRibbon.getButton("Tab", "Panel", buttonName2);
 
      // Set Size and Text Visibility
      btnTest1.Size = RibbonItemSize.Large;
      btnTest1.ShowText = false;
      btnTest2.Size = RibbonItemSize.Large;
      btnTest2.ShowText = false;
      
      // Return StackedItem
      return ribbonItem;
    }
  }
}
</pre>


####<a name="4"></a> Reinitialise the Filtered Element Collector

Yet another important hint
from Frank [@Fair59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518) Aarssen
on reinitialising the filtered element collector
for [collection of elements created using `ElementWorksetFilter` giving incorrect count](https://forums.autodesk.com/t5/revit-api-forum/collection-of-elements-created-using-elementworksetfilter-giving/m-p/9164018):

You need to reinitialise a filtered element collector before reusing it.
All the filters that you add to it are accumulated.
If they are mutually exclusive, you will get zero results.

**Question:** I'm trying to retrieve empty worksets, but the count method of the collection of elements in a particular workset is not giving correct results.
Here is my code:

<pre class="code">
  FilteredElementCollector fec = new FilteredElementCollector(doc);
  FilteredWorksetCollector fwc = new FilteredWorksetCollector(doc);
  fwc.OfKind(WorksetKind.UserWorkset);

  try
  { 
    string msg = "";
    int count = 0;
    Transaction t = new Transaction(doc);
    t.Start("Check Empty Worksets");
    foreach (Workset w in fwc)
    {
      ElementWorksetFilter ewf = new ElementWorksetFilter(w.Id, false);
      ICollection<ElementId> elemIds = fec.WherePasses(ewf).ToElementIds();
      int foundElems = elemIds.Count;
      TaskDialog.Show("Elements:", w.Name + ": " + foundElems.ToString());
      if(foundElems == 0)
      {
        count++;
        msg += count.ToString() + ". " + w.Name + "\n";
      }
    }
    if (count == 0) msg = "None";
    TaskDialog.Show("Empty Worksets: ", msg);

    t.Commit();
    t.Dispose();
  }
  catch(Exception e)
  {
    TaskDialog.Show("Error", e.ToString());
  }
</pre>

**Answer:** A `FilteredElementCollector` isn't a static variable, but a dynamic collection.

Every time you apply a filter, the elements that don't pass the filter are removed from the collection.

So, after the first pass of the `foreach` loop, the collector only contains the elements belonging to the first workset.

All those elements aren't part of the second workset (2nd pass) and therefore the collector is empty after the second pass. 

Solution: reinitialize the collector in every pass:

<pre class="code">
foreach (Workset w in fwc)
{
	ElementWorksetFilter ewf = new ElementWorksetFilter(w.Id, false);
	ICollection<ElementId> elemIds = new FilteredElementCollector(doc).WherePasses(ewf).ToElementIds();
	int foundElems = elemIds.Count;
	count++;
	msg += foundElems.ToString() + ". " + w.Name + "\n";
}
</pre>

By the way, you might also want
to [simplify your transaction handling by wrapping it in a `using` statement](https://thebuildingcoder.typepad.com/blog/2012/04/using-using-automagically-disposes-and-rolls-back.html).
However, I also wonder whether you need any transaction at all for this read-only operation.

This misunderstanding caused a similar initial problem in another recent case involving 
a [material assets collector for appearance, structural (physical) and thermal](https://forums.autodesk.com/t5/revit-api-forum/material-assets-collector-appearance-structural-physical-amp/m-p/7256944).

