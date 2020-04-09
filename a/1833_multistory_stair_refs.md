<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

twitter:

Zoom tips, jobs at Autodesk, BIM 360 GET project users API and multistory stair point element references in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon https://bit.ly/multistorystarirpointrefs

I hope you and your friends and family are all safe, sound and managing well.
Our main solution today reveals a new trick handling multistory stairs
&ndash; Point element references in multistory stairs
&ndash; BIM 360 <code>GET</code> project users API
&ndash; Zoom tips
&ndash; Jobs at Autodesk...


linkedin:

Zoom tips, jobs at Autodesk, BIM 360 GET project users API and multistory stair point element references in the #RevitAPI

https://bit.ly/multistorystarirpointrefs

I hope you and your friends and family are all safe, sound and managing well.

Our main solution today reveals a new trick handling multistory stairs:

- Point element references in multistory stairs
- BIM 360 <code>GET</code> project users API
- Zoom tips
- Jobs at Autodesk...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Multistory Stair Point References, BIM360 Project Users, Zoom and Jobs

I hope you and your friends and family are all safe, sound and managing well.

My own situation is pretty much business as usual, being used to the home office for a number of decades now.
I am fine and enjoying the quieter times around me.

Our main solution today reveals a new trick handling multistory stairs:

- [Point element references in multistory stairs](#2)
- [BIM 360 `GET` project users API](#3)
- [Zoom tips](#4)
- [Jobs at Autodesk](#5)

#### <a name="2"></a>Point Element References in Multistory Stairs

Kyle Faulkner raised and solved an interesting issue involving references to points in multistory stairs in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [point element and its reference are in different locations in multistory stair](https://forums.autodesk.com/t5/revit-api-forum/point-element-and-its-reference-are-in-different-locations-in/m-p/9415962):

**Question:** I'm writing a script in Dynamo to dimension stairs and I'm having a problem with reference points in multistory stairs.

The points from `get_Geometry` are returned correctly, but the references for the points that belong to the 'copied' stairs are referencing the points of the original stair.

Here is a little script illustrating this:

<center>
<img src="img/multistair_geom_pts_dyn.png" alt="Multistory stair points" title="Multistory stair points" width="800"/> <!-- 1054 -->
</center>

It is simply taking the point, finding its reference, and grabbing the geometry from that reference.

For the 'copied' stairs, this returned point is not the input point, as I would expect, but rather the corresponding point on the original stair.

Is this a bug? The point is the best reference for me for the dimension, but if there is an alternative I could use I am looking for suggestions.
Otherwise, my fall back is to draw a detail line and dimension to that.

**Answer:** Glad to hear that you have a viable fallback.

In addition, I hope that you can glean some useful further ideas from
the [extensive discussion on multistory stairs and its challenges](https://forums.autodesk.com/t5/revit-api-forum/multistorey-stair-subements/m-p/8349447) that
sound partially related to the one you are facing.

**Response:** Thank you this is great info.
Don't know how I missed it in my searching.

While it doesn't help to figure out what is happening with the points, it gives me an idea to use that point data to find a face in the geometry at the same elevation (which would be top of landing) and use that as a reference instead of using a detail line.

On a related note, when I get the top face of a landing by this path: Stairs &gt; StairsLanding &gt; Geometry &gt; Face &gt; Reference, the resulting dimension is invisible.

But if I use this path: Stairs &gt; Geometry &gt; Face &gt; Reference, the created dimension is visible as expected.

Not sure why this is the case.
I tried some of the previous solutions, such as creating a new dimension with using the references of the invisible dimension, but that didn't work.

Later:

AH HA!!  I think I have found the problem.

My script was set up to take a multistair and get the unique stairs within that multistair via GetAllStairsIds().
(This is important, because the multistair can have unpinned stairs in it and I need all the unique stairs within that multistair so that I can get the proper info I need).
From there, I would get the geometry from each stair (and its copies if they exist) and attempt to get the references from the points that define the run, as shown in my original post.
This is where the reference from the 'copied' stairs would wrongly return the reference to the corresponding point on the original stair.

BUT...

If I get the geometry from the multistair element directly, and acquire the references from the points from that geometry, all the references are in the correct position in relation to the point. SUCCESS!

Only problem is now I have to query the geometry twice: once from the individual stairs, so that I can maintain a relationship between the points of the runs and the information pulled from that run, and then once from the multistory stair, so that I can match the points from the first query to the points from this second query to get the correct references.

Hopefully this makes sense.

Many thanks to Kyle for this tricky solution and helpful explanation!

#### <a name="3"></a>BIM 360 `GET` Project Users API

Very briefly, just a quick pointer to Mikako's blog post to let you know that
the long-awaited [BIM 360 `GET` project users API is now available](https://forge.autodesk.com/blog/get-project-users-bim-360-finally-here).


#### <a name="4"></a>Zoom Tips

Staying at home and still keeping in touch with others... 
everybody seems to be using [Zoom](https://zoom.us) nowadays.

Here are some neat [tips](zip/zoom_tips.pdf) to make it more fun and effective:

- Add fun with a virtual background:
Need to hide a messy background, or just add some fun?
Try the virtual background option! 
Click the gear icon by your profile picture to access your settings and click Virtual Background in the left navigation.
Choose from the default background images or upload your own. 
- How to use whiteboarding and annotations:
It is often useful to show a visual representation of what you are discussing.
You can access the whiteboard via Share Screen &gt; Whiteboard.
Use the toolbar to write, stamp, erase, and more.
You can also annotate a shared screen using the Annotate option on your meeting controls.
- Unmute Zoom with the spacebar:
Have you ever started speaking in an online meeting and then realised that no one can hear you because you're on mute?
Then, there's the frantic mouseing to try to unmute yourself.
Next time just quickly unmute by holding down your keyboard spacebar while you talk;
when you release it, you're automatically muted again.  
- Access Zoom chats after the meeting: Chats for Zoom meetings without a cloud recording are automatically saved on your computer in a Documents &gt; Zoom subfolder. You can also set things up to save chats in a cloud recording.
- Two ways to invite people quickly: use 'Copy URL' in the invitation dialogue, or automatically copy the invite URL when starting a meeting.
​​​There is an option to have the meeting URL automatically copied to your clipboard whenever a meeting starts via
General settings  &gt; Automatically copy Invitation URL to Clipboard after meeting starts.

#### <a name="5"></a>Jobs at Autodesk

As we continue chugging along in spite of the pandemic, Autodesk is also searching for new talent.

For instance, we currently have two job offers for senior software engineers in Switzerland:

- [Senior Software Engineer, Switzerland, Job ID 20WD38612](https://rolp.co/AC5Ih)
- [Senior Software Engineer, Switzerland, Job ID 20WD38613](https://rolp.co/VuEgi)

The position overview and responsibilities are the same for both:

The Autodesk Construction Solutions group is seeking a talented and highly motivated individual to develop a world-class commercial cloud service that is used by construction companies to increase efficiency and reduce the high amount of waste produced during construction. If you are a software developer proficient in cloud web services with large database back-ends and enjoy working in a dynamic, fast-paced team with state-of-the-art technologies, we would love to hear from you!

Responsibilities:

- Involved with all aspects of software development.
- Developing with quality and working to create and maintain the most reliable, secure, performant and high throughput service for our customers by leveraging cutting-edge technology
- Partner closely with product managers, product owners, software architects and other stakeholders to iteratively turn high level requirements into product enhancements that are delivered to customers incrementally and continually
- Contribute to software design and architecture by leveraging cloud design patterns and injecting your cloud expertise into the entire development lifecycle
- Contribute to improvements in processes and deliverables that increase the effectiveness and efficiency of the team in responding to customer and business needs
- Document and present your ideas and solutions accurately and thoroughly

Good luck applying for one of these or the many other opportunities that you can find all over the world in
the [Autodesk career site](https://www.autodesk.com/careers)!
