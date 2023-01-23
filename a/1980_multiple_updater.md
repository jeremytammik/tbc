<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- insight and experience handling multiple updaters
  and iterative use of SetExecutionOrder
  an interesting background information based on several man-years of experience by two important contributors 
  Can the method "SetExecutionOrder" by used to set the order of more than two IUpdaters
  https://forums.autodesk.com/t5/revit-api-forum/can-the-method-quot-setexecutionorder-quot-by-used-to-set-the/m-p/11683732#M68598

- Richard provided another solution to solve the question 
  Try block not catching owner/permission locks
  https://forums.autodesk.com/t5/revit-api-forum/try-block-not-catching-owner-permission-locks/m-p/11683634#M68596

- Speckle [Chuong Ho: Featured Developer](https://speckle.systems/blog/chuong-ho-featured-developer)
 
- Community Conversations
  https://www.linkedin.com/feed/update/urn:li:activity:7021162362904735744?utm_source=share&utm_medium=member_desktop
  past events
  https://forums.autodesk.com/t5/community-conversations/eb-p/communityconversations?include_past=true
  upcoming
  https://forums.autodesk.com/t5/community-conversations/eb-p/communityconversations?include_upcoming=true&include_ongoing=true
  > Community Conversations are webinars led by Community members on their areas of expertise, hosted by Community Managers. Join a session to delve into product features, discover innovative workflows, learn tools and techniques, and make connections in the Community.

- Parameters API ... nice documentation
  https://aps.autodesk.com/en/docs/parameters/v1/overview/introduction/
  The Parameters API will work alongside the Revit API to load parameters from the service into Revit projects and families.

twitter:

&ndash; 
...

linkedin:

#bim #DynamoBim #AutodeskAPS #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### Handling Multiple Updaters

<center>
<img src="img/.jpg" alt="" title="" width="100"/> <!-- 800 × 514 pixels -->
</center>

####<a name="2"></a> 

Thank you for asking, 

**Question:** 

**Answer:** 

**Response:** 

####<a name="2"></a> Community Conversation Roadmap AMAs

Last week saw a bunch of roadmap discussions looking at future ideas and directions of different areas of Revit and Dynamo in <i>Ask me Anything</i> format:

- Architecture and Platform Product (Revit) Roadmap Ask Me Anything
Join us for a Community Conversation about the future of Autodesk AEC solutions. - Learn about highlights from recent releases, including Revit and the AEC Collection - Ask product managers and experts about the future of Autodesk AEC products - Check the roadmap and join the conversation! Panelists: Harlan Brumm Autodesk Sr. Product Line Manager Mike Engel Autodesk  Sr. Product Manager Bogdan Matei Autodesk Revit Product Manager Matt Arsenault Autodesk Revit Product Manager Sam Anderson of Twinmotion by Epic Games Event Recording: https://youtu.be/HLrrGEtBKEM
Tuesday, January 17, 2023
- Computational Design and Automation (Dynamo) Roadmap Ask Me Anything
Have questions about the public roadmap for Computational Design and Automation using Dynamo? We've got answers! Join us for a Community Conversation about the future of Dynamo and Autodesk AEC solutions. - Learn about highlights from recent releases - Ask product managers and experts about the future of Dynamo Panelists: Lilli Smith Autodesk  Sr. Product Manager Sol Amour Autodesk Product Line Mgr. Jacob Small Autodesk Global Consulting Delivery  Event recording: https://youtu.be/LfERZO3Fdzg
Tuesday, January 17, 2023
- Infrastructure Product Roadmap Ask Me Anything
Have questions about the public roadmap for civil infrastructure products? We've got answers! Join us for a Community Conversation about the future of Autodesk AEC solutions.   - Learn about highlights from recent releases, including Civil 3D, InfraWorks, ReCap Pro, and the AEC Collection - Ask product managers and experts about the future of Autodesk AEC products - Check the roadmap and join the conversation! Panelists: Dave Simeone Autodesk Sr. Product Manager, Civil Products Tim Yarris Autodesk Product Manager – Civil 3D Ramesh Sridharan Autodesk Product Manager – ReCap, InfraWorks Recording:  https://youtu.be/t2tuGOqax-w  See the Civil Infrastructure Public Roadmap: https://trello.com/b/tafIRGcN/autodesk-civil-infrastructure-product-roadmap Join the Infrastructure Futures https://feedback.autodesk.com/key/InfrastructureFutures
Wednesday, January 18, 2023
- Structures Product Roadmap Ask Me Anything
Have questions about the public roadmap for structural engineering products? We've got answers! Join us for a Community Conversation about the future of Autodesk AEC solutions.   - Learn about highlights from recent releases, including Revit and the AEC Collection - Ask product managers and experts about the future of Autodesk AEC products - Check the roadmap and join the conversation! Panelists: Pawel Piechnik Autodesk Director, Product Management, Structures Product Line Dan Peticila Autodesk Product Manager Event recording: https://youtu.be/JJ53a1EKFm8 See the Revit Public Roadmap: https://blogs.autodesk.com/revit/roadmap/
Thursday, January 19, 2023
- MEP Product Roadmap Ask Me Anything
Have questions about the product roadmap for MEP products? Join Autodesk product managers and get them answered live.   - Learn about highlights from recent releases, including Revit and the AEC Collection - Ask product managers and experts about the future of Autodesk AEC products - Join the conversation!   Panelists: Martin Schmid Autodesk Product Line Manager Event recording: https://youtu.be/y3sHuWQqtYc Submit questions before the event for the team to answer: https://admin.sli.do/event/7mLL71KUV1j85pHW2uYCoJ/polls    Register Now to Attend: https://autodesk.zoom.us/meeting/register/tJYkcuqorzIoEtD9lq9xGRUryaaki5baUaLM See the Revit Public Roadmap: https://blogs.autodesk.com/revit/roadmap/
Thursday, January 19, 2023

Check them out in
the [community-conversation recordings of past events](https://forums.autodesk.com/t5/community-conversations/eb-p/communityconversations?include_past=true).

####<a name="3"></a> Parameters API

Talking about roadmaps and upcoming changes, one important and long-awaited enhancement revolves around the handling of parameter definitions.

The Autodesk Platform Services APS (formerly Forge) documentation includes a very nice beta documentation of
the [new Parameters API](https://aps.autodesk.com/en/docs/parameters/v1/overview/introduction) from
the web side of things.

> The Parameters API will work alongside the Revit API to load parameters from the service into Revit projects and families.

Please note that this API is in beta and its current documentation is intended for beta users only:

> Almost every Autodesk application has its own mechanism to manage parameters (properties and attributes are the alternative name for the same type of constructs). Some applications let the customers define their own parameters & properties. For example, Revit uses a concept called shared parameters. These are currently locked away in text files that are not intended to be edited. Revit users are required to manage the files on local devices or shared network drives. This opens this data to the risk of being edited accidentally and that the wrong data is being used. There is no central source of truth for parameters and properties which spans the diversity of tools used to deliver a design project.

> The Parameters Service lets you store and manage parameter definitions in the cloud. You can easily manage your parameters in one place and organize them using new capabilities with collections, labels and saved searches. The built-in search capability enables your users to quickly discover specific parameters or narrow down suitable options for meaningful consumption. The Parameters Service also lets you set defaults for values like Revit Categories, type vs instance, and property palette group.

> A collection of parameters will always be up to date and in sync across various products and services within the Autodesk ecosystem. Only administrators, those with properly assigned permissions, within Autodesk Construction Cloud manage parameter definitions, while collaborators, and team members can explore and load parameter definitions into their connected application.

> As of Autodesk Revit 2023, the Parameters Service is available in both Revit and for account administrators in the Autodesk Construction Cloud Library. For more information
see [Technology Preview: Parameters Service](https://help.autodesk.com/view/RVT/2023/ENU/?guid=GUID-073AB0E7-64BF-4A6E-9E67-59D3709266C3).



####<a name="4"></a> insight and experience handling multiple updaters

Moving more to the nitty-gritty aspects of Revit API programming,
Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
and [Chris Hildebran](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2814832) exchanged
some valuable background information based on many man-years of experience working with
the [Dynamic Model Updater Framework DMU](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.31)
and ended up presenting an iterative use of the `SetExecutionOrder` method in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
asking [Can `SetExecutionOrder` be used to set the order of more than two IUpdaters?](https://forums.autodesk.com/t5/revit-api-forum/can-the-method-quot-setexecutionorder-quot-by-used-to-set-the/m-p/11683732):

**Question:** Looking at
the [SetExecutionOrder method](https://www.revitapidocs.com/2023/87d62116-cdb4-efc4-e2e2-e4f5b41b3441.htm)...
We have about eight IUpdaters in our addin and have the need to specify the order in which they fire.

Has anyone had this need and if so, do you know if you can set the order of more than two?

I was thinking a "Two at a time" approach setting the order of one pair (as allowed using the SetExecutionOrder) IUpdaters then continuing this for the remaining in a list each time incrementing an index. 

Since every updater adds a performance hit to the model processing and execution time, so we have already done some work to consolidate them.

Fortunately, the IUpdater system we have set up is working fine (aside from the execution order need) even on large modes (+1GB) but...
the system does have a code smell.
We do address a bit of technical debt in each sprint:
When can we do it is the question.
We have quite a backlog of cool things to implement. 

Perhaps I should have prefaced my OP by mentioning that we have consolidated our IUpdaters to the quantity we have currently, and that further consolidation is forthcoming.
Until then we'd like to control the execution order. 

**Solution:** It appears you can use the `SetExecutionOrder` iteratively.

The list you pass in has to be ordered in the desired order.

So, until i can consolidate a bit more this will work.

public void SetExecutionOrder(List<IUpdater> updaters)
{
var count = updaters.Count;

for (var i = 0; i < count; i++)
{
var previous = updaters[i].GetUpdaterId();

var last = updaters[i + 1].GetUpdaterId();

UpdaterRegistry.SetExecutionOrder(previous, last);
}
}

**Update:** To better handle the situation where the count of items (IUpdaters) is odd it was better to implement a do...while loop.

public void SetExecutionOrder(List<IUpdater> updaters)
	{
		var last     = updaters[updaters.Count - 1];
		var lastLoop = false;
		var index    = 0;

		do
		{
			var current = updaters[index];  

			var next = updaters[index + 1];  

			lastLoop = next == last;  

			UpdaterRegistry.SetExecutionOrder(current.GetUpdaterId(), next.GetUpdaterId());

			index++;
		}
		while (lastLoop == false);
	}

**Further discussion:**

Out of interest could you give a brief description of why you have implemented eight updaters in the single add-in?

We never know 100% for sure that an updater will not find itself disabled by Revit due to an unforeseen issue. I think therefore we can't really assume that one will execute before another we can only tell that if it does execute it will be before the other.

Therefore for me the execution order should be largely irrelevant in the design of the DMU since you can't overly rely on it. Also the update is all predefined the moment the update is triggered ideally you would do the most you could in the single execute method. Perhaps the only advantage of setting the order is to limit the retriggering, is that your aim?

Tags (0)
Add tags
Report
MESSAGE 8 OF 13
TheRealChrisHildebran
 Advocate TheRealChrisHildebran in reply to: RPTHOMAS108
‎2023-01-16 03:06 PM 
I want to reiterate that the number of IUpdaters hasn't had a noticeable negative effect on our model performance. Many of which approach 1GB in size. On the contrary, I'm confident our detailing department would revolt if taken away. 😊 Their inclusion offers so much productivity its worth whatever the performance hit is.

Soon after my post-dynamo start down the road of c# programming in 2017, I learned about these updaters and implemented several simple ones based on examples from the Autodesk website and The Building Coder website.

The several became a half dozen but were monstrous implementations with far too much "cyclomatic complexity". Most likely because of my inexperience in OOP, Revit API, and short-sightedness.

Around 2020 I split them up into, I'm guessing, about two dozen updaters. And subsequently pared down a bit more.

Today, they are lightweight and unobtrusive little micro-services with distinct work scopes. I could likely combine/refactor, and probably that review will happen later this year.

As far as updaters becoming disabled or their firing order, I'm not 100% convinced that they will continue to fire in the order I desire for the duration of the Application Session. Still, for all the testing so far, the updaters are firing in the order I asked them to.

I'm hopeful that soon I will be able to refactor the remaining DMU's into a better design.
Tags (0)
Add tags
Report
MESSAGE 9 OF 13
RPTHOMAS108
 Mentor RPTHOMAS108 in reply to: TheRealChrisHildebran
‎2023-01-16 03:36 PM 
That's fine was just curious about your motivation for the order and the numbers of updaters. I never used that method probably wasn't available historically. I assume it is only included so you can better prevent one updater triggering another.

I always recall a situation from years ago when an end user colleague pinned down an odd delay in Revit to the precast add-in (as it was then a separate thing from main Revit). Whenever you moved something concrete or otherwise there was this blue circle on the screen for a period of time (it was probably about three to four seconds). I think in the course of a day it got really irritating.  So the add-in was uninstalled and people have been warry of it ever since. I recall there was this Red on/off button for it and as soon as you turned it on that was it (your day just got longer).

There are quire a few instances like that; small delays were people start looking for culprits. Kind of surprised Autodesk don't take a similar approach to Microsoft by now. In outlook etc. when an add-in takes up a lot of processing time it reports that and the user can understand where the time is spent (otherwise it is considered a Revit issue by default). I think in the MS Outlook case it is checking start-up time but in theory time spent executing particular DMU's could be considered by Revit over the course of a session.

Tags (0)
Add tags
Report
MESSAGE 10 OF 13
TheRealChrisHildebran
 Advocate TheRealChrisHildebran in reply to: TheRealChrisHildebran
‎2023-01-16 04:04 PM 
The primary reason at the moment is to force one of the updaters to run last. It's a new updater that is updating all construction data from cached SQL Server data. We are now only storing a primary key on a family or Fabrication Part. The construction data will be written to non user-modifiable, project bound, Shared Parameters. Gone will be the days of stale data stored in one of our 5000 rfa's and Fabrication Database. Our content manager is both happy and scared. 🙂

Tags (0)
Add tags
Report
MESSAGE 11 OF 13
RPTHOMAS108
 Mentor RPTHOMAS108 in reply to: TheRealChrisHildebran
‎2023-01-16 04:23 PM 
Sounds interesting.  Similarly in theory we could avoid all information for COBie in Revit and just have keys. Then form the spreadsheet based on those keys and the information held elsewhere.

I often wonder about the history of keys and the information they point to however. When a key is stored it points to an item in a database but was that item defined in the database the same way as when later referenced with the key (you would expect not otherwise why have a key)? That is both the advantage and the disadvantage of it. It could be the historic information is lost and the key now points to misleading (since updated) version of the item in the database. When do you essentially need to define a new key, when is the product significantly different from what it was. Probably you have to keep the old information unchanged anyway for historic purposes.

Tags (0)
Add tags
Report
MESSAGE 12 OF 13
TheRealChrisHildebran
 Advocate TheRealChrisHildebran in reply to: RPTHOMAS108
‎2023-01-16 05:11 PM 
In our case the database Id's will persist indefinitely. A record may be set as obsolete, however.

We've been slowly turning the ship with regards to construction data for about 3 years. This sprint is the final adjustment.

It given us time to discover and adjust our needs and goals as a company and grow confident that this direction is the correct one.

At least until AU 2023 where everything will change! 🙂

####<a name="3"></a> Richard provided another solution to solve the question

Try block not catching owner/permission locks
https://forums.autodesk.com/t5/revit-api-forum/try-block-not-catching-owner-permission-locks/m-p/11683634#M68596

####<a name="3"></a> Speckle [Chuong Ho: Featured Developer](https://speckle.systems/blog/chuong-ho-featured-developer)

