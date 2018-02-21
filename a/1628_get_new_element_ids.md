<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- retrieving newly created elements:
  https://stackoverflow.com/questions/48869272/python-revit-part-utils-how-to-get-append-the-results-out
  https://forums.autodesk.com/t5/revit-api-forum/how-to-get-a-merged-part-after-merging-with-some-parts/td-p/7772648
I believe I just answered a very similar question in the Revit API discussion forum thread on [how to get a merged part after merging with some parts](https://forums.autodesk.com/t5/revit-api-forum/how-to-get-a-merged-part-after-merging-with-some-parts/td-p/7772648):
You can subscribe to [the DocumentChanged event](http://www.revitapidocs.com/2018.1/988dd6cf-fcaa-85d2-622d-c50f13917a13.htm) just before calling CreateParts, and unsubscribe just afterwards.
That will tell you all the element ids added to the database during the call.
This is demonstrated in the discussion on [retrieving newly placed family instances](http://thebuildingcoder.typepad.com/blog/2010/06/place-family-instance.html).

Retrieving newly created element ids in #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/new_element_ids

An add-in will often need to retrieve the elements that it just created for further processing.
Frequently, the Revit API method used to create them will return their element ids.
Sometimes, that is not the case.
Now this topic arose again in a couple of cases and brought some other aspects to mind
&ndash; Using the element lister
&ndash; Consecutive element ids
&ndash; Retrieving recently added elements
&ndash; AEC job openings in Munich and elsewhere...

--->

### Retrieving Newly Created Element Ids

An add-in will often need to retrieve the elements that it just created for further processing.

Frequently, the Revit API method used to create them will return their element ids.

Sometimes, that is not the case.

We already discussed a simple and effective method to retrieve all newly created elements following a call to
the [`PromptForFamilyInstancePlacement` method](http://www.revitapidocs.com/2018.1/b05a17df-3f63-9172-8e49-d2e1e6b8e9e2.htm) by
subscribing to the [`DocumentChanged` event](http://www.revitapidocs.com/2018.1/988dd6cf-fcaa-85d2-622d-c50f13917a13.htm) just
beforehand in the discussion
on [placing family instances](http://thebuildingcoder.typepad.com/blog/2010/06/place-family-instance.html).

Now this topic arose again in a couple of cases and brought some other aspects to mind:

- [Using the element lister](#2)
- [Consecutive element ids](#3)
- [Retrieving recently added elements](#4)
- [AEC job openings in Munich and elsewhere](#5)


####<a name="2"></a>Using the Element Lister

The [element lister](http://thebuildingcoder.typepad.com/blog/2014/09/debugging-and-maintaining-the-image-relationship.html#2) provides
an easy way to discover element relationships between related objects that are added to Revit by certain operations.

It is included
in [AdnRevitApiLabsXtra, the ADN Revit API Training Labs including Xtra](https://github.com/jeremytammik/AdnRevitApiLabsXtra).

All it does is grab all elements in the entire database and list whatever properties you are interested in, e.g., element id, category, level, .NET class, etc.

Then you can use
a [Unix `diff` tool](https://en.wikipedia.org/wiki/Diff_utility) to
compare states before and after doing your thing.

It can be well combined with
the [built-in parameter checker BipChecker](https://github.com/jeremytammik/BipChecker) to explore the properties in further depth.

Of course,
the [most intimate database exploration](http://thebuildingcoder.typepad.com/blog/2013/11/intimate-revit-database-exploration-with-the-python-shell.html) requires
a live read-evaluate-print console.

####<a name="3"></a>Consecutive Element Ids

Using any of the techniques listed above, you will quickly determine that Revit element ids are basically consecutive numbers.

There is no guarantee for this, of course, and they can be mixed up by work-sharing operations.

They can only be used to identify an element within one single Revit document.

Because they are assigned consecutively, Revit automatically generates
many [undocumented `ElementId` relationships](http://thebuildingcoder.typepad.com/blog/2011/11/undocumented-elementid-relationships.html).

For more persistent identification, a `UniqueId` is recommended.

For the sake of completeness, Revit elements have two identifiers:

- [Element id `ElementId`](http://www.revitapidocs.com/2018.1/44f3f7b1-3229-3404-93c9-dc5e70337dd6.htm)
- [Unique id `UniqueId`](http://www.revitapidocs.com/2018.1/f9a9cb77-6913-6d41-ecf5-4398a24e8ff8.htm)

We discuss their uses in lots of places, e.g.,
[understanding the use of the `UniqueId`](http://thebuildingcoder.typepad.com/blog/2015/02/understanding-the-use-of-the-uniqueid.html).

For connecting with an external database, I would suggest using the UniqueId.

One simple sample that shows the whole connection strategy is provided by
the [FireRatingCloud add-in](https://github.com/jeremytammik/FireRatingCloud).


####<a name="4"></a>Retrieving Recently Added Elements

So let us summarise the above again to answer
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [how to get a merged part after merging with some parts](https://forums.autodesk.com/t5/revit-api-forum/how-to-get-a-merged-part-after-merging-with-some-parts/td-p/7772648) and
the StackOverflow question
on [Python Revit `Part.Utils` how to get append the results out](https://stackoverflow.com/questions/48869272/python-revit-part-utils-how-to-get-append-the-results-out):

**Question 1:** I have merged some parts using `PartUtils.CreateMergedPart`.

The output is not a `Part`, but a `PartMaker`.

I need a part output.

Is there any way to get a merged part object?

**Question 2:** I am using `PartUtils.CreateParts` to create parts and would like to collect the results as a list of element ids.

How can I achieve that, please?

**Answer:** As said above, you can subscribe to
the [`DocumentChanged` event](http://www.revitapidocs.com/2018.1/988dd6cf-fcaa-85d2-622d-c50f13917a13.htm) just before calling `CreateParts`, and unsubscribe just afterwards.

That will tell you all the element ids added to the database during the call.

This is demonstrated in the discussion on [retrieving newly placed family instances](http://thebuildingcoder.typepad.com/blog/2010/06/place-family-instance.html).

<center>
<img src="img/road_narrows.png" alt="Merge parts" width="211"/>
</center>

####<a name="5"></a>AEC Job Openings in Munich and Elsewhere

I see Autodesk offering several AEC related job openings.

Below are two located in Munich, Germany, for offsite and home office.

For more, please check
the [Autodesk job search site](https://autodesk.taleo.net/careersection/adsk_gen/jobsearch.ftl).

#####<a name="6"></a> [Industry Program Manager, AEC](https://autodesk.taleo.net/careersection/adsk_gen/jobdetail.ftl?job=18WD26910&tz=GMT%2B00%3A00)

Requisition ID: 18WD26910

Are you interested in a marketing job at Autodesk and wondering if this is the right one? Start by answering these five questions:

- Are you one of those unique people who can see the big picture and look after the smallest of details all at the same time?
- Do you like complex and sometimes ambiguous problems, and digging for data and insights that might help solve those problems?
- Do you work with a sense of purpose and urgency, motivating others to join your mission?
- Do you like working on a wide variety of projects, and find doing the same work day after day boring?
- Are you continually challenging yourself to do more, do it better, and do it faster?

If you said yes to all these questions, you may be the perfect fit for the role of Industry Program Manager for AEC (Architecture, Engineering, and Construction).

Position Overview

This is a unique individual contributor position at Autodesk. Your job is to make the EMEA cross-functional marketing teams (Account-based Marketing, Lead Gen Marketing, Digital Direct, Channel Marketing) successful by being the interface between them and the global industry strategy teams. You will coordinate industry strategy, programs and assets across the different sales channels (ABM, Digital Direct, Territory).

You will work with smart, global industry teams that are responsible for building and managing marketing programs and campaigns for our AEC industries. It will be your mission to:

- Communicate and coordinate the execution of campaigns across the country teams
- Gather input from the field
- Represent EMEA Demand Gen teams back to the industry strategy teams

It requires a unique combination of marketing experience, business acumen, industry know-how, project management, communication, and leadership skills. And at the heart of it, you have love to get things done.

Responsibilities

- Communicate global programs and campaigns to align marketing initiatives in EMEA and execute to the AEC strategic vision
- Ensure the availability of assets for Account-based Marketing, Lead Gen Marketing, Digital Direct teams and support Channel Marketing in communicating strategic goals to partners
- Lead the annual planning process for EMEA with the key Sales, Marketing and Industry stakeholders
- Develop and deliver professional written and spoken communications that ensure team members and leadership have clear, concise, and up-to-date information on progress
- Monitor performance of campaigns and recommend changes to continuously improve them
- Ensure clear definition of program success and present results against targets on a regular basis to Sales and Marketing leaders in EMEA
- Identify and communicate program scope and ensure critical paths, milestones and all objectives are met
- Work with teams and individuals to mitigate conflicts and resolve issues as needed. Leverage cross-organizational relationships for support and escalate in a savvy manner
- Collaborate with industry business development groups

Minimum Qualifications

- Excellent communication & collaboration skills and organizational talent to manage projects from start to end successfully
- Business acumen and industry knowledge to drive thought-leadership for the Architecture, Engineering, and Construction
- Change champion, motivator, and coach; demonstrated success delivering meaningful results through teams
- Strong analytical and problem-solving skills
- Excellent relationship management and expectation setting skills
- Familiarity with selling concepts and best practices; experience working with sales teams
- Strong ability to manage and prioritize multiple, concurrent tasks while meeting aggressive deadlines in a fast-paced environment with unending optimism
- Proficient with MS Office tools and familiar with Marketo and/or SFDC
- Comfortable with remote video collaboration tools e.g., Skype, Zoom, Go-to-Meeting, etc.
- BA/BS degree in marketing or communications
- 5+ years professional marketing experience in the high-tech industry (software industry experience preferred)

#####<a name="7"></a> [BIM Implementation Consultant](https://autodesk.taleo.net/careersection/adsk_gen/jobdetail.ftl?job=17WD25466&tz=GMT%2B00%3A00)

Requisition ID: 17WD25466

Position Overview

As a BIM Technical Consultant, you will be essential for working directly with our customers and developing/contributing to their BIM implementation strategy (in collaboration with our sales, consulting delivery and business development teams) for our major account customers.

The position combines your pre-sales and post-sales consultancy skills, your construction industry experience, your technical and professional know-how, and your passion for educating customers on Autodesk’s Construction BIM solutions.

Responsibilities

- Carry out the implementation of Autodesk's BIM solution on projects, collaborating with Autodesk sales and consulting delivery and business development teams
- Analyse customer business workflows and document findings
- Define, construct, and carry out the adoption of BIM implementation plans, leveraging your industry experience and best practices
- Manage customer relationships on implementation projects
- Document, share and promote BIM team best practices for redistribution and consistent implementation at a corporate level
- Own a part of the BIM Solution Portfolio corresponding to your area of expertise
- Identify opportunities for new projects to enhance customer effectiveness in their own business

Minimum Qualifications

- Strong experience in the Construction Industry
- Broad understanding, garnered through work in practice, of the Construction  industries throughout the asset lifecycle
- Broad understanding of our Autodesk technology around BIM 360 Field, Glue, Production planning, Point Layout, Naviswork
- Understand the Construction Business Process around Production Planning, Scheduling, Commissioning, hand over to owners
- Understand BIM principles and at least 2 years of hands-on experience implementing BIM in an enterprise and/or on projects
- Experience in software product adoption Experience in business process improvement principles, and implementing change in complex organizations
- Fluent oral and written in English and German. Candidates with additional language skills would have a clear advantage
- Excellent verbal and written communication skills; ability to communicate complex technical details coherently
- Able to travel and enjoy working with diverse groups of people with widely variable technical skills

Preferred Qualifications

- Mature, goal-oriented, takes initiative and responsible
- Does not mind working on multiple projects at a time
- Able to act independently while working on a multi-disciplinary and geographically diverse team
- Can effectively communicate with this team, even with remote and international team members
