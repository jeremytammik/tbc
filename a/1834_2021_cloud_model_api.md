<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

Phil Xia 
In Revit 2021, our team updated the Revit API to support multi-region, including US and Europe, and also changed the SaveAsCloudModel interface, which accept more arguments from ForgeDM. I am wondering if you can add a new page in your blog to explain the new Cloud Model API and its changes to support Multi-Region in 2021?
I wrote one here, and maybe we can refine it to fit the public standard.
https://wiki.autodesk.com/pages/viewpage.action?pageId=629756479

twitter:

 in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon

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

### Revit 2021 Cloud Model API

Revit 2021 has now been released.

The Revit 20201 SDK is available from the [Revit Developer Centre]()

#### <a name="2"></a>

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

**Question:**

**Answer:** 

**Response:** 

#### <a name="3"></a>BIM 360 

Very briefly, just a quick pointer to Mikako's blog post to let you know that
the long-awaited [BIM 360 `GET` project users API is now available](https://forge.autodesk.com/blog/get-project-users-bim-360-finally-here).

#### <a name="4"></a>Revit 2021 Multi-Region Cloud Model API

How to use the updated Revit 2021 Cloud Model API with multi-region projects and models, by Phil Xia, Team Manager, Autodesk.

#### <a name="4.1"></a>Background &ndash; Revit Cloud Model Multi-Region

In Revit 2021, we released the Revit Cloud Model for Multi-Region feature.

Autodesk provides two different BIM 360 web portals and regions with different URLs:

- [BIM 360 US, at insight.b360.autodesk.com](https://insight.b360.autodesk.com)
- [BIM 360 EU, at insight.b360.eu.autodesk.com](https://insight.b360.eu.autodesk.com)

Now you can save your Revit cloud models to either of these two.

<center>
<img src="img/cloud_model_api_1.png" alt="Cloud Model API" title="Cloud Model API" width="100"/> <!-- 1342>
</center>

Revit 2021 combines the BIM 360 accounts of the current login user, from different regions together in all cloud models entrances:

- Revit Home
- Save as Cloud Model dialog
- Cloud model initialize dialog
- Manage cloud model dialog

So, Revit users can create (upload and saveas) cloud models, authoring models from different regions in one Revit session.

#### <a name="4.2"></a>Identifying the Region

There is no access to determine which region the BIM 360 accounts/projects/models belongs to.
This was determined based on USer research, interviews and analytic data.

- BIM managers can set up naming conventions for their BIM 360 accounts and projects to indicate the region.
- Only a small percentage of BIM 360 users have both EU and US region BIM 360 accounts.

#### <a name="4.3"></a>Revit Cloud Model API Changes in Revit 2021

ModelPath

The new property

Region
returns the region of the BIM 360 account and project which contains this model.

Cloud Model API changes

ModelPathUtils.ConvertCloudGUIDsToCloudPath(Guid projectGuid, Guid modelGuid)

ModelPathUtils.ConvertCloudGUIDsToCloudPath(string region, Guid projectGuid, Guid modelGuid)
The new method provides an extra argument to indicate the region of the model to open.
Document.SaveAsCloudModel(string folderId, string modelName)	Document.SaveAsCloudModel(Guid accountId, Guid projectId, string folderId, string modelName)	The new method provides two extra arguments to identify the cloud location more efficient and reliable.
How to open a cloud model
The region argument for ConvertCloudGUIDsToCloudPath is a string type, it should be either "US" or "EMEA", which depend on what's the region of BIM 360 account and project this model stored in.

US	https://insight.b360.autodesk.com	ModelPathUtils.CloudRegionUS
EU	https://insight.b360.eu.autodesk.com	ModelPathUtils.CloudRegionEMEA
So if you know where does the cloud model store, BIM 360 US or EU, and you can give the region argument as "US" or "EMEA" respectively.

Note: Why not provide the region as an ENUM type, like Region {US, EMEA}?

We may on-board a new region in the middle of Revit release cycle, this is one AC for Revit Cloud Model Multi-Region Support Project. Revit since 2021 has the ability to fetch the supported region from cloud and automatically supports new region when the cloud infrastructure was ready.

To convert a valid CloudPath with Revit API ModelPathUtils.ConvertCloudGUIDsToCloudPath, you need to register a Forge application and use Forge Data management API to get the project Guid and model Guid as the other 2 arguments.

Please check out the Forge DM API reference - https://forge.autodesk.com/en/docs/data/v2/reference/http/hubs-GET/ - which list the hubs your Forge application can access.

Here is the response body of this list hubs API, and you can filter out your interested BIM 360 account by data.attributes.name field, and also you can get the region information here, it is "EMEA" in this case.  

<pre class="prettyprint">
  {
    "type": "hubs",
    "id": "b.6bdabd18-6096-492b-966e-86492a4bb660",
    "attributes": {
      "name": "Wookong_EU",
      "extension": {
        "type": "hubs:autodesk.bim360:Account",
        "version": "1.0",
        "schema": {
          "href": "https://developer.api.autodesk.com/schema/v1/versions/hubs:autodesk.bim360:Account-1.0"
        },
        "data": {}
      },
      "region": "EMEA"
    },
    "links": {
      "self": {
        "href": "https://developer.api.autodesk.com/project/v1/hubs/b.6bdabd18-6096-492b-966e-86492a4bb660"
      }
    },
    "relationships": {
      "projects": {
        "links": {
          "related": {
            "href": "http://developer.api.autodesk.com/dm/v1/hubs/b.6bdabd18-6096-492b-966e-86492a4bb660/projects"
          }
        }
      }
    }
  }
</pre>

Please check out the Forge DM API reference - https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-folders-folder_id-contents-GET/ - which list the folder contents you plan to open.

Here is the response body of this list folder content API, and you can filter out your interested cloud model by included.attributes.name field, and also you can get the project Guid and model Guid information in included.attributes.extension.data field.

<pre class="prettyprint">
  "included": [
    {
      "type": "versions",
      "id": "urn:adsk.wipbimemeastg:fs.file:vf.VFlOMhozRMac61hJ1JB_Nw?version=1",
      "attributes": {
        "name": "C4R_12_11_2019_10_01_14 AM_28.rvt",
        "displayName": "C4R_12_11_2019_10_01_14 AM_28.rvt",
        "createTime": "2019-12-11T10:01:44.0000000Z",
        "createUserId": "YZVYJQWWAJ89",
        "createUserName": "Phil Xia",
        "lastModifiedTime": "2019-12-11T10:01:47.0000000Z",
        "lastModifiedUserId": "YZVYJQWWAJ89",
        "lastModifiedUserName": "Phil Xia",
        "versionNumber": 1,
        "mimeType": "application/vnd.autodesk.r360",
        "extension": {
          "type": "versions:autodesk.bim360:C4RModel",
          "version": "1.1.0",
          "schema": {
            "href": "https://developer.api.autodesk.com/schema/v1/versions/versions:autodesk.bim360:C4RModel-1.1.0"
          },
          "data": {
            "modelVersion": 3,
            "projectGuid": "fd1335eb-733b-480c-9d16-1a22e742ef70",
            "modelType": "singleuser",
            "latestEpisodeGuid": "11da0d90-e1bb-492a-b90e-f3759ca6ab39",
            "mimeType": "application/vnd.autodesk.r360",
            "modelGuid": "e5a59497-0d79-4df0-879d-396310288bb0",
            "processState": "PROCESSING_COMPLETE",
            "extractionState": "SKIPPED",
            "splittingState": "NOT_SPLIT",
            "revisionDisplayLabel": "1",
            "sourceFileName": "C4R_12_11_2019_10_01_14 AM_28.rvt"
          }
        }
      },
</pre>

With these three pieces of information &ndash; region, project guid and model guid &ndash; you can convert a valid cloud path with the new ModelPathUtils.ConvertCloudGUIDsToCloudPath method and then open this model with OpenDocument or OpenAndActivateDocument methods.

How to save a local file to cloud as Non-workshared Cloud Model

To save a local Revit file to cloud as Non-workshared cloud model, you need to get the BIM 360 account id, BIM 360 project id, BIM 360 folder id, and a model name. There are 2 ways to get these information.

1 . Get these information in web browser.

Open a web browser and go to your BIM 360 project home page as the picture below, and you can copy the url highlight.

<center>
<img src="img/cloud_model_api_2.png" alt="Cloud Model API" title="Cloud Model API" width="100"/> <!-- 1079 -->
</center>

Here is the mapping, and you can get the BIM 360 account id and project id in this way, both of them are guid strings.

<center>
<img src="img/cloud_model_api_3.png" alt="Cloud Model API" title="Cloud Model API" width="100"/> <!-- 1202 -->
</center>

Then go to your target BIM 360 Docs folder in web browser, like the picture below, and copy the URL highlight.
<center>
<img src="img/cloud_model_api_4.png" alt="Cloud Model API" title="Cloud Model API" width="100"/> <!-- 1104 -->
</center>

Here is the mapping, and you can get the BIM 360 folder id in this way, and it is "urn:adsk.wipemea:fs.folder:co.Jo68ieLRRcKvQr4fI2Q8uQ" in this case.

<center>
<img src="img/cloud_model_api_5.png" alt="Cloud Model API" title="Cloud Model API" width="100"/> <!-- 1274 -->
</center>

With this information, you can save a local file which opened in Revit to BIM 360 Document management as a cloud model.

<pre class="prettyprint">
currentDocument.SaveAsCloudModel(
	new Guid("a8d3b76e-cf23-4dd7-a090-9e893efcf949"), 			// BIM 360 account id.
	new Guid("bf46f5e3-285e-496f-be03-b5b1f8b1e154"), 			// BIM 360 project id.
	"urn:adsk.wipemea:fs.folder:co.Jo68ieLRRcKvQr4fI2Q8uQ",  	// BIM 360 folder id.
	"rac_advanced_sample_project.rvt"							// model name.
);
</pre>

2 . Get these information with Forge DM API

With your Forge application, you can 

List hubs with https://forge.autodesk.com/en/docs/data/v2/reference/http/hubs-GET/ api, and you can get region and you BIM 360 account Id.
List projects  with https://forge.autodesk.com/en/docs/data/v2/reference/http/hubs-hub_id-projects-GET/ api, and you can get all the projects of the given hub, and you can get BIM 360 project Id.
List top folders with https://forge.autodesk.com/en/docs/data/v2/reference/http/hubs-hub_id-projects-project_id-topFolders-GET/ api, and you can get all the top folders you have the permissions, and you can get a valid folder Id here.
or continue to get the nested folders with list folder content https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-folders-folder_id-contents-GET/ api, until you find your target folder and keep the folder Id for later use.
With these information, you can save a local file which opened in Revit to BIM 360 Document management as cloud model as method #1 code snippet.
