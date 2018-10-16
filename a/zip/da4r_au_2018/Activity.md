# Activity

An `activity` is an action which can be executed in Design Automation. You create and post your own activities to run against target appbundles.


### APIs for activity

This page will explain the first of the sample `activity` APIs below:
 
- Command &ndash; Endpoint URL &ndash; Description
- **POST** &ndash; **/v3/activities** &ndash; [Creates a new activity](#create-a-new-activity)
- **POST** &ndash; **/v3/activities/{id}/aliases** &ndash; Creates a new alias for this activity
- **POST** &ndash; **/v3/activities/{id}/versions** &ndash; Creates a new version of the activity
- **PATCH** &ndash; **/v3/activities/{id}/aliases/{aliasId}** &ndash; Modify alias details

The other three are similar to the [`appbundle` APIs](AppBundle.html).


## Create a New Activity

To create a new `activity` with the id `DeleteWallsActivity`, post this request:

<pre>
curl -X POST \
  https://developer.api.autodesk.com/da/us-east/v3/activities \
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer LongStringAccessTokenObtainedDuringAuthenthication' \
  -d '{
            "id": "DeleteWallsActivity",
            "commandLine": [ "$(engine.path)\\\\revitcoreconsole.exe /i $(args[rvtFile].path) /al $(appbundles[DeleteWallsApp].path)" ],
            "parameters": {
              "rvtFile": {
                "zip": false,
                "ondemand": false,
                "verb": "get",
                "description": "Input Revit model",
                "required": true,
                "localName": "$(rvtFile)"
              },
              "result": {
                "zip": false,
                "ondemand": false,
                "verb": "put",
                "description": "Results",
                "required": true,
                "localName": "result.rvt"
              }
            },
            "engine": "Autodesk.Revit+2018",
            "appbundles": [ "YourNickname.DeleteWallsApp+test" ],
            "description": "Delete walls from Revit file."
    }'
</pre>

- `id` &ndash; The name given to your new activity.
- `commandLine` &ndash; The command run by this activity.
- `$(engine.path)\\\\revitcoreconsole.exe` - The full path to the folder from which the engine for Revit executes. 
- `engine` &ndash; The engine on which your activity runs. 


#### Response

<pre>
{
    "commandLine": [
        "$(engine.path)\\\\revitcoreconsole.exe /i $(args[rvtFile].path) /al $(appbundles[DeleteWallsApp].path)"
    ],
    "parameters": {
        "rvtFile": {
            "verb": "get",
            "description": "Input Revit model",
            "required": true,
            "localName": "$(rvtFile)"
        },
        "result": {
            "verb": "put",
            "description": "Results",
            "required": true,
            "localName": "result.rvt"
        }
    },
    "engine": "Autodesk.Revit+2018",
    "appbundles": [
        "YourNickname.DeleteWallsApp+test"
    ],
    "description": "Delete walls from Revit file.",
    "version": 1,
    "id": "YourNickname.DeleteWallsActivity"
}
</pre>

The response to the new `activity` post includes:

- `version` &ndash; The version number for the activity created by the POST request. For the Post request creating a new activity, version always returns `1`.

