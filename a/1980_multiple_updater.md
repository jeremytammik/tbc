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


####<a name="3"></a> insight and experience handling multiple updaters

and iterative use of SetExecutionOrder
an interesting background information based on several man-years of experience by two important contributors 
Can the method "SetExecutionOrder" by used to set the order of more than two IUpdaters
https://forums.autodesk.com/t5/revit-api-forum/can-the-method-quot-setexecutionorder-quot-by-used-to-set-the/m-p/11683732#M68598

####<a name="3"></a> Richard provided another solution to solve the question

Try block not catching owner/permission locks
https://forums.autodesk.com/t5/revit-api-forum/try-block-not-catching-owner-permission-locks/m-p/11683634#M68596

####<a name="3"></a> Speckle [Chuong Ho: Featured Developer](https://speckle.systems/blog/chuong-ho-featured-developer)

####<a name="3"></a> Parameters API

... nice documentation
https://aps.autodesk.com/en/docs/parameters/v1/overview/introduction/
The Parameters API will work alongside the Revit API to load parameters from the service into Revit projects and families.

