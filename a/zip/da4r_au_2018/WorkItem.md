# Workitem

When you post a workitem to Design Automation, you are requesting a job to be run on Design Automation.

A workitem is used to execute an activity. The relationship between an activity and a workitem can be thought of as a 'function definition' and 'function call', respectively. Named parameters of the activity have corresponding named arguments of the workitem. Like in function calls, optional parameters of the activity can be skipped and left unspecified while posting a workitem.

## APIs for workitem

This page will explain some sample `workitem` APIs below.

- Command &ndash; Endpoint URL &ndash; Description
- **POST** &ndash; **/v3/workitems** &ndash; [Creates a new workitem and queues it for processing](#post-a-workitem).
- **GET** &ndash; **/v3/workitems/{id}** &ndash; [Gets the status of a specific workitem](#workitem-status).


## POST a Workitem

Here is an example of a workitem that executes the `DeleteWallsActivity`.

<pre>
curl -X POST \
  https://developer.api.autodesk.com/da/us-east/v3/workitems \
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer LongStringAccessTokenObtainedDuringAuthenthication' \
  -d '{
        "activityId": "YourNickname.DeleteWallsActivity+test",
        "arguments": {
          "rvtFile": {
            "url": "https://s3-us-west-2.amazonaws.com/revitio-dev/test-data/DeleteWalls.rvt"
          },
          "result": {
            "verb": "put",
            "url": "https://myWebsite/signed/url/to/result"
          }
        }
      }'
</pre>

`LongStringAccessTokenObtainedDuringAuthenthication` needs to be replaced with your authentication token string.

- `activityId` &ndash; The target activity defined by "owner.activity+alias"(`YourNickname.DeleteWallsActivity+test`) this workitem will execute.
- `arguments` &ndash; The argument list that is required by the activity.
- `rvtFile` - It is the URL to get the input file that will be processed by the workitem.
- `result` - It is the URL to which the output will be 'put' (uploaded).


### Reponse

The HTTP response will contain the `id` of the posted workitem.
<pre>
{
    "status": "pending",
    "stats": {
        "timeQueued": "2018-04-16T21:45:08.1357163Z"
    },
    "id": "e8a3ee53770a4eaeb86f267aab76af47"
}
</pre>


## Workitem Status

Design Automation workitems are queued before they are processed.  Processed workitems may have run successfully or may have failed execution.

You can check the status of a workitem by calling `[GET] /workitems/{id}`:

<pre>
curl -X GET \
  https://developer.api.autodesk.com/da/us-east/v3/workitems/e8a3ee53770a4eaeb86f267aab76af47 \
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer LongStringAccessTokenObtainedDuringAuthenthication'
</pre>

`LongStringAccessTokenObtainedDuringAuthenthication` needs to be replaced with your authentication token string.

Notes: The `workitemId` must be changed to use your workitem id. 

### Response

The Response is like below:
<pre>
{
    "status": "success",
    "reportUrl":  "https://dasprod-store.s3.amazonaws.com/workItem/Revit/e8a3ee53770a4eaeb86f267aab76af47/report.txt?XXXXXXXXX",
    "stats": {
        "timeQueued": "2018-04-13T03:15:15.9772282Z",
        "timeDownloadStarted": "2018-04-13T03:15:17.2960823Z",
        "timeInstructionsStarted": "2018-04-13T03:15:20.2803318Z",
        "timeInstructionsEnded": "2018-04-13T03:15:41.6075799Z",
        "timeUploadEnded": "2018-04-13T03:15:42.0450494Z"
    },
    "id": "e8a3ee53770a4eaeb86f267aab76af47"
}
</pre>

- `status` &ndash; Indicates if execution is pending, successful, failed or cancelled.
- `reportUrl` &ndash; The URL to get the report log for this workitem's execution.
- _`progress`_ &ndash; _A place holder for future use. You can ignore this now._


## Arguments: More Support 

### Input Arguments: Embedded JSON 

If an input argument of an activity requires JSON values, the JSON values can be embedded in the workitem itself.  

For example, the activity `CountItActivity` requires an parameter named `countItParams`. The activity expects the argument value to be a JSON file. The workitem is able to embed the JSON values in the workitem itself as below.  
By prefixing those values with `data:application/json,`, it instructs the Design Automation framework to treat them as JSON stream and save them as a JSON file.

<pre>
curl -X POST \
  https://developer.api.autodesk.com/da/us-east/v3/workitems \
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer LongStringAccessTokenObtainedDuringAuthenthication' \
  -d '{
          "activityId": "YourNickname.CountItActivity+test",
          "arguments": {
            "rvtFile": {
              "url": "https://s3-us-west-2.amazonaws.com/revitio-dev/test-data/CountIt.rvt"
            },
            "countItParams": {
              "url": "data:application/json,{'walls': false, 'floors': true, 'doors': true, 'windows': true}"
            },
            "result": {
              "verb": "put",
              "url": "https://myWebsite/signed/url/to/result"
            }
          }
      }'
</pre>

### Input Arguments: ETransmit Files

Design Automation is capable of processing outputs from ETransmit for Revit, so long as you first create a zip file from those outputs.

<pre>
curl POST \
  https://developer.api.autodesk.com/da/us-east/v3/workitems \
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer LongStringAccessTokenObtainedDuringAuthenthication' \
  -d '{
        "activityId" : "YourNickname.CountItActivity+test",
        "arguments": {
          "rvtFile": {
            "url": "https://s3-us-west-2.amazonaws.com/revitio-dev/test-data/TopHost.zip"
          },
          "countItParams": {
            "url": "data:application/json,{'walls': true, 'floors': true, 'doors': true, 'windows': true}"
          },
          "result": {
            "verb": "put",
            "url": "https://myWebsite/signed/url/to/result"
          }
        }
    }'
</pre>

The name of the 'Root Model' is read from the manifest file. The root model is then found in the zip.


### Host RVT File with Linked Models

<pre>
curl POST \
  https://developer.api.autodesk.com/da/us-east/v3/workitems \
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer LongStringAccessTokenObtainedDuringAuthenthication' \
  -d '{
        "activityId": "YourNickname.CountItActivity+test",
        "arguments": {
          "rvtFile": {
            "url": "https://s3-us-west-2.amazonaws.com/revitio-dev/test-data/TopHost.rvt",
            "references": [
              {
                "url": "https://s3-us-west-2.amazonaws.com/revitio-dev/test-data/LinkA.rvt",
                "references": [
                  {
                    "url": "https://s3-us-west-2.amazonaws.com/revitio-dev/test-data/LinkA1.rvt"
                  },
                  {
                    "url": "https://s3-us-west-2.amazonaws.com/revitio-dev/test-data/LinkA2.rvt"
                  }
                ]
              },
              {
                "url": "https://s3-us-west-2.amazonaws.com/revitio-dev/test-data/LinkB.rvt"
              }
            ]
          },
          "countItParams": {
            "url": "data:application/json,{'walls': true, 'floors': true, 'doors': true, 'windows': true}"
          },
          "result": {
            "verb": "put",
            "url": "https://myWebsite/signed/url/to/result"
          }
        }
    }'

</pre>

The root model in this example is `TopHost.rvt` and it contains `LinkA.rvt` and `LinkB.rvt`. The file `LinkA.rvt` in turn contains `LinkA1.rvt` and `LinkA2.rvt`. Each of these files are uploaded to a Cloud location and the path is provided for each individually.

<pre>
TopHost.rvt
|-- LinkA.rvt
|   |-- LinkA1.rvt
|   |-- LinkA2.rvt
|
|-- LinkB.rvt  
</pre>


### RvtLinks in Sub-Folders

The workitem's `localName` variable can be used to create a folder structure inside the working directory. For example, a Revit file **Host.rvt** containing a relative link **SubFolder/Link.rvt** can be defined in this way for `rvtFile` in the workitem:

<pre>
{
  "url": "https://s3-us-west-2.amazonaws.com/revitio-dev/test-data/TestForSubFolders/Host.rvt",
  "references": [
    {
      "url": "https://s3-us-west-2.amazonaws.com/revitio-dev/test-data/TestForSubFolders/Link.rvt",
      "localName": "SubFolder/Link.rvt"
    }
  ]
}
</pre>

This will create the directory/file structure in the current working directory (CWD):

<pre>
{CWD}/Host.rvt
{CWD}/SubFolder/Link.rvt
</pre>

Because you are not allowed to create a folder structure outside of your current working directory, if the host file has linked files with relative paths like **../ParallelFolder/Link.rvt**, you can move the entire structure down one level by creating a top level folder of your own. The same `localName` variable can be used for the top host like you use for linked files. Here is an example json.

<pre>
{
 "url": "https://path/to/Host.rvt",
 "localName": "TopFolder/Host.rvt",
 "references": [
   {
     "url": "https://path/to/Link.rvt",
     "localName": "ParallelFolder/Link.rvt"
   }
 ]
}
</pre>

This will create the directory/file structure:

<pre>
{CWD}/TopFolder/Host.rvt
{CWD}/ParallelFolder/Link.rvt
</pre>

### Output Arguments: onComplete callback

Each `workitem` is furnished with a special output argument named `onComplete`.  When provided, the callback URL will be called on completion of the workitem.

Here is an example of how to call `[POST] /workitems`, which adds `onComplete` argument to an earlier [example](#post-a-workitem).

<pre>
curl -X POST \
  https://developer.api.autodesk.com/da/us-east/v3/workitems \
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer LongStringAccessTokenObtainedDuringAuthenthication' \
  -d '{
        "activityId": "YourNickname.DeleteWallsActivity+test",
        "arguments": {
          "rvtFile": {
            "url": "https://s3-us-west-2.amazonaws.com/revitio-dev/test-data/DeleteWalls.rvt"
          },
          "result": {
            "verb": "put",
            "url": "https://myWebsite/signed/url/to/result"
          },
          "onComplete": {
            "verb": "post",
            "url": "https://myWebsite/callback"
          }
        }
      }'
</pre>

> This argument is not `required` to be provided on every `[POST] /workitems` call.

On completion of the workitem, the sepecified url is called with a payload identical to the response received on `[GET] /workitems/{id}` call. 

Since the nature of the implementation of the callback url is similar to how one may implement a callback url for [Webhooks API](https://developer.autodesk.com/en/docs/webhooks/v1/overview/), you may refer the Webhooks API documentation. You may also find the documentation for [configuring local server](https://developer.autodesk.com/en/docs/webhooks/v1/tutorials/configuring-your-server/) helpful.

