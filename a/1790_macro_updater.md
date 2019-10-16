<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- IUpdater in a project macro on startup
  https://forums.autodesk.com/t5/revit-api-forum/iupdater-in-a-project-macro-on-startup/m-p/9087481


twitter:

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Dynamic Model Updater Macro

####<a name="2"></a>

Dave raised and solved an interesting issue in an area that we have not discussed much here yet in
his [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [IUpdater in a project macro on startup](https://forums.autodesk.com/t5/revit-api-forum/iupdater-in-a-project-macro-on-startup/m-p/9087481):

**Question:**

I'm trying to use `IUpdater` in a macro that automatically starts when a project opens.
Is this possible?

So far, I used Macro Manager / Create to set up some boilerplate.
Then I pasted in Autodesk's `WallUpdater` example code from the knowledge article
on [Implementing IUpdater](https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2015/ENU/Revit-API/files/GUID-6D434229-0A2E-41FE-B29D-1BB2E6471F50-htm.html)
into my `public partial class ThisDocument`.

I figured out how to run code on project startup by calling it from the boilerplate's `private void Module_Startup`.

But I haven't had any luck calling `WallUpdater`.

Here is my code:

<pre class="code">
using System;
using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace test
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.DB.Macros.AddInId("redacted")]
	public partial class ThisDocument
	{
		private void Module_Startup(object sender, EventArgs e)
		{
			TaskDialog.Show("hello","this pops up when you open the project");
		}

		private void Module_Shutdown(object sender, EventArgs e)
		{

		}

		#region Revit Macros generated code
		private void InternalStartup()
		{
			this.Startup += new System.EventHandler(Module_Startup);
			this.Shutdown += new System.EventHandler(Module_Shutdown);
		}
		#endregion
		
		public class WallUpdaterApplication : Autodesk.Revit.UI.IExternalApplication
		{
			public Result OnStartup(Autodesk.Revit.UI.UIControlledApplication application)
			{
				// Register wall updater with Revit
				WallUpdater updater = new WallUpdater(application.ActiveAddInId);
				UpdaterRegistry.RegisterUpdater(updater);

				// Change Scope = any Wall element
				ElementClassFilter wallFilter = new ElementClassFilter(typeof(Wall));

				// Change type = element addition
				UpdaterRegistry.AddTrigger(updater.GetUpdaterId(), wallFilter, Element.GetChangeTypeElementAddition());
				return Result.Succeeded;
			}

			public Result OnShutdown(Autodesk.Revit.UI.UIControlledApplication application)
			{
				WallUpdater updater = new WallUpdater(application.ActiveAddInId);
				UpdaterRegistry.UnregisterUpdater(updater.GetUpdaterId());
				return Result.Succeeded;
			}
		}

		public class WallUpdater : IUpdater
		{
			static AddInId m_appId;
			static UpdaterId m_updaterId;
			WallType m_wallType = null;

			// constructor takes the AddInId for the add-in associated with this updater
			public WallUpdater(AddInId id)
			{
				m_appId = id;
				m_updaterId = new UpdaterId(m_appId, new Guid("FBFBF6B2-4C06-42d4-97C1-D1B4EB593EFF"));
			}

			public void Execute(UpdaterData data)
			{
				Document doc = data.GetDocument();

				// Cache the wall type
				if (m_wallType == null)
				{
					TaskDialog.Show("hello", "world");
				}

				if (m_wallType != null)
				{
					TaskDialog.Show("hello", "world");
				}
			}
			
			public string GetAdditionalInformation()
			{
				return "Wall type updater example: updates all newly created walls to a special wall";
			}

			public ChangePriority GetChangePriority()
			{
				return ChangePriority.FloorsRoofsStructuralWalls;
			}

			public UpdaterId GetUpdaterId()
			{
				return m_updaterId;
			}

			public string GetUpdaterName()
			{
				return "Wall Type Updater";
			}
		}
	}
}
</pre>

**Answer:** You should probably not start messing around with this directly in OnStartup.

The system will not work until the Revit application has been fully initialised first, i.e., until
the [ApplicationInitialized method](https://www.revitapidocs.com/2020/f35ba9fc-0b6b-4284-60eb-91788761127c.htm) has been called.

I also think that the updater is associated with a specific document.

Consequently, you should probably wait until a document is available, i.e., until
the [DocumentOpened event](https://www.revitapidocs.com/2020/7e5bc7a1-0475-b2ec-0aec-c410015737fe.htm) has been called.

How about starting with a working sample first, and understanding the system in a bit more depth before rolling your own?

Here are some [samples and discussion of DMU](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.31).

**Response:** All the samples I've found appear to be written as add-ins.

Do you happen to know where I can find a sample of `IUpdater` in a macro?

**Answer:** Yes, actually, and surprisingly, I do,
in [wrangling revisions with Ruby](https://thebuildingcoder.typepad.com/blog/2014/02/wrangling-revisions-with-ruby.html).

**Response:** That's a bit too complex for me to follow at this point, especially in Ruby.

I was hoping to find an RVT with a vanilla C# `IUpdater` macro embedded, similar to the SDK *Revit_Macro_Samples.rvt*.
Oh well. 

The Boost your BIM article
on [automatically running API code when your model changes](https://boostyourbim.wordpress.com/2012/12/17/automatically-run-api-code-when-your-model-changes) looks
promising.

Later: Yes, I got it working!

Here are the steps I followed:

1. Use MacroManager to create a new C# module.

2. Edit the new module in SharpDevelop.

3. Add RegisterUpdater() to Module_Startup as follows:

private void Module_Startup(object sender, EventArgs e)
{
	RegisterUpdater();
}

4. Add UnregisterUpdater() to Module_Shutdown as above.

5. Just below the boilerplate #endregion, add the boostyourbimcode :

public class FamilyInstanceUpdater : IUpdater
{ ... }
public void RegisterUpdater()
{ ... }
public void UnregisterUpdater()
{ ... }
Make sure FamilyInstanceUpdater, RegisterUpdater, and UnregisterUpdater are inside the scope of ThisDocument.

For testing, I found it handy to replace boostyourbim's Execute code with:

public void Execute(UpdaterData data)
{
	Document doc = data.GetDocument();
	TaskDialog.Show("Revit","hello");
}
6. Save and hit F9 to build the macro. Check the MacroManager to see if the build was successful. For some reason, the build occasionally fails for me. I've found that having AssemblyInfo.cs open in SharpDevelop helps. No idea why.

At this point, the macro should be working. Any time you draw an element, you'll get a little popup that says 'hello'.

If you close your project with a successfully built macro, the macro will run automatically the next time you open the project.

I'm sure lots of improvements can be made. For instance, Jeremy recommended using DocumentOpened. And my version currently works for columns and beams, but not for walls. Any input would be appreciated.

Meanwhile, thanks very much to Jeremy for taking a look and to boostyourbim for their code. Hope this helps someone.

<center>
<img src="img/.png" alt="" width="271">
</center>

**Answer:**

<pre class="code">
</pre>

**Response:** 

####<a name="5"></a> 
