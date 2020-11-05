<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


twitter:

#RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### 


####<a name="2"></a> 

**Question:** 

<center>
<img src="img/.png" alt=" title="" width="100"/> <!-- 837 -->
</center>

**Answer:** 

**Response:** 

####<a name="3"></a> 

####<a name="4"></a> AU Classes for Construction Customers

Are you specifically interested in construction?
Check out the overview
of [AU classes for construction customers](https://forge.autodesk.com/blog/forge-au-classes-construction).

####<a name="5"></a> Retrieve Sheet Metadata in Forge Viewer

Now, let's turn to Forge.
Here is a pretty illuminating exploration on accessing Revit sheet metadata in that environment:

<!-- https://autodesk.slack.com/archives/C0LP63082/p1602524162022900 -->

**Question:** I am trying to retrieve the 'Identity Data' of a sheet in a Revit model using the Model Derivative API:

<center>
<img src="img/sheet_identity_data.png" alt="Sheet identity data" title="Sheet identity data" width="553"/> <!-- 1107 -->
</center>

Unfortunately, I was unable to find this info anywhere in the properties.
Is it possible, or do I have to use Design Automation for this?
Thanks!

**Answer:**  It should be possible to get sheet properties by navigating the hierarchy of the object tree.
The root node (`id` = `1`) is the document, and the sheets will be listed as children of that root.
One would need to iterate through the children of the root to get the properties of the sheets and views.

**Response:** I do see the model as `id` = `1`, but I can't find sheets as children of that root.
I also do not see `Sheet` in the model browser in the Forge viewer in any example (which certainly contain Revit models having sheets).
Am I overlooking something?

**Answer:** The model browser will not show sheets, because they do not have physical geometry associated with them.
However, there will be sheet objects as children of the root.

Maybe, in some RVTs, sheets do not appear in the property database, though.
It probably depends on which API you use to get properties.
I'm referring to the full property database available to the Forge viewer.

**Response:** This is an export of the tree of `/metadata` for my Revit model:

<pre class="code">
Untitled 
{
"data": {
  "type": "objects",
  "objects": [
      {
      // ... (2,059 lines)
</pre>

Here are the `/properties`:

<pre class="code">
Untitled 
{
"data": {
  "type": "properties",
  "collection": [
      {
      // ... (20,859 lines)
</pre>

These come from a sheet called "A102 - Plans".

**Answer:** I don't know what subset of element properties are returned by the Forge properties API.
I do know that the Forge viewer will show sheet properties in some cases, e.g., in BIM360 Docs when you open the sheet.

**Response:** Yes indeed, BIM 360 Docs does show properties of sheets.
I checked and confirmed.
Now I wonder how it gets those properties.

**Answer:** Just like I said &ndash; it loops through the children of the root node and finds the sheet element with the matching name.
However, it's not using the Forge properties API.
It uses the raw property data, available to the Forge Viewer

**Response:** I kind of understand what you say.
I understand that the properties are being retrieved by the raw property data.
However, to first select the element id, the hierarchy (from the `/metadata` endpoint) should retrieve sheets, right?
I don't see sheets in that response; or is it that there's also other raw data which is different from the Forge metadata API?

**Answer:** The Forge metadata endpoint is not raw, it's processed data.
From the above, it looks like it's missing the child properties that will let you easily find the sheets from the root element.

**Response:** Thanks, this is very helpful.
Final question: can this raw data be accessed by a customer?

**Answer:** Yes, using Forge Viewer.
It may be possible to get this information via metadata somehow that I am not aware of.

**Response:** Hmm... so, if I want to query and fetch attribute values, that won't be possible using Forge viewer, right?

**Answer:** The MD service does let you perform queries to get the metadata you want, with two choices of data format.
If you run into data that MD does not collect, and Revit Design Automation would be your fallback.

Here is an example accessing additional metadata,
to [extract compound structure layer from RVT files using Design Automation for Revit](https://github.com/augustogoncalves/forge-customproperty-revit).

The resources listed for the [Forge at AU 2020 pre-event online bootcamp](https://forge.autodesk.com/blog/forge-au-2020-pre-event-online-bootcamp) will probably also be useful for you.

####<a name="6"></a> Determining the BIM 360 Project Id

Kevin Augustino very kindly shared his current approach
to [retrieve the BIM 360 Document Management Project Id of the active Revit cloud model](https://forums.autodesk.com/t5/revit-api-forum/bim-360-document-management-project-id-of-revit-cloud-model/m-p/9830419):

**Question:** How can I retrieve the BIM 360 Document Management Project Id of the active Revit model?
I'm aware of *Document.GetCloudModelPath().GetProjectGUID()*, but this seems to be a C4R Project Id.
I need the Document Management Id to interface with
the [Forge BIM 360 and Data Management APIs](https://forge.autodesk.com/en/docs/bim360/v1/reference/http/).

So far, I've found that the Document Management file has an attribute that matches the C4R Project Guid: *attributes.extension.data.projectGuid*.

So, I need to find the Docs project that contains a file such that:

<pre>
  attributes.extension.data.projectGuid
    = &lt;ActiveRevitDocument&gt;.GetCloudModelPath().GetProjectGUID().
</pre>

But surely there's a better approach than doing a [folder search](https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-folders-folder_id-search-GET/) using a filter matching *filter[attributes.extension.data.projectGuid]* with `ValueFromCloudModelPath` on every Docs Project that my Forge App has access to?

**Answer:** I asked the development team for you whether they can suggest a better way.
They are currently discussing the implementation of a direct method to retrieve the BIM 360 project id of the document via a property such as `Document.ProjectId`, now as we speak. It will hopefully be available in a future release of Revit.

Meanwhile, the convoluted approach you describe sounds significantly better than nothing at all to me, so well done finding a way through the maze.

 **Response:** For anyone else who runs into this same need, here are some of my other findings:

`Document.PathName` seems to be a string in this form when opening a cloud model:

<pre>
  BIM 360://&lt;DocsProjectName&gt;/&lt;ModelName&gt;.rvt
</pre>

So, another option is to try parsing `Document.PathName` to get the Document Management Project name:

<pre class="code">
  string regexPattern =
    @"^BIM 360:\/\/(?&lt;ProjectName&gt;.*)\/(?&lt;ModelName&gt;.*)$";

  if (Regex.IsMatch(doc.PathName, regexPattern))
  {
    Match match = Regex.Match( doc.PathName, regexPattern );
    string projectName = match.Groups["ProjectName"].Value;
  }
</pre>

Then look for a project with that name by iterating each hub returned
from *https://forge.autodesk.com/en/docs/data/v2/reference/http/hubs-GET/*,
and, on each one, try
to [get a project using a name filter](https://forge.autodesk.com/en/docs/data/v2/reference/http/hubs-hub_id-projects-GET/), 
using a filter such as

<pre>
  string.format( "?filter[attributes.name]={0}",
    HttpUtility.UrlEncode(projectName))
</pre> 

If this project name isn't unique, then this approach might not get the correct one.
But additional processing can be applied to use a folder search looking for

<pre>
  attributes.extension.data.projectGuid
    = &lt;ActiveRevitDocument&gt;.GetCloudModelPath().GetProjectGUID()
</pre>

So at least this way, the folder search is only done on potential matches, rather than every single project.

If the Document Management project name changes, then `Document.PathName` won't refresh to the new project name until you re-save the model.
So, as a fallback, if I still haven't found the project Id, I resort to the folder search on every project regardless of name.

Not ideal, but hopefully a direct method will be added to the Revit API in the future!

Many thanks to Kevin for all his research and documentation work on this!

####<a name="7"></a> AI Solves Partial Differential Equations

[AI has cracked a key mathematical puzzle for understanding our world](https://www.technologyreview.com/2020/10/30/1011435/ai-fourier-neural-network-cracks-navier-stokes-and-partial-differential-equations):

> Partial differential equations can describe everything from planetary motion to plate tectonics, but they’re notoriously hard to solve...

> They can be used to model everything from planetary orbits to plate tectonics to the air turbulence that disturbs a flight, which in turn allows us to do practical things like predict seismic activity and design safe planes...

> PDEs are notoriously hard to solve...

> Researchers at Caltech have introduced a new deep-learning technique for solving PDEs,
a [Fourier Neural Operator for Parametric
Partial Differential Equations](https://arxiv.org/pdf/2010.08895.pdf)
... dramatically more accurate... much more generalizable ... 1'000 times faster ...

####<a name="8"></a> AI-Enhanced Video Editing 

Here is another example of AI usage that may come in handier to you right away than solving differential equations:

AU is coming up. Are you possibly thinking about recording a video?
Check out [Descript](https://www.descript.com) before you do.
It is a collaborative audio and video editor that includes transcription, a screen recorder, publishing, full multitrack editing, and some mind-bendingly useful AI tools:

- [Blog post](https://medium.com/descript/introducing-descript-fa37eb193819)
- [Video](https://youtu.be/Bl9wqNe5J8U):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/Bl9wqNe5J8U" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center>
