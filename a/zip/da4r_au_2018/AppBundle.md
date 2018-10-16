# App

An `appbundle` is the package of binaries and supporting files which make your Revit 
Addin application.

## Appbundle Structure

Design Automation API for Revit expects your `appbundle` to be a zip file with certain contents.  Here is the zip file for a sample appbundle called DeleteWallsApp.zip:

<pre>
DeleteWallsApp.zip
|-- DeleteWalls.bundle
|   |-- PackageContents.xml
|   |-- Contents
|   |   |-- DeleteWalls.dll
|   |   |-- DeleteWalls.addin
</pre>


The top-level folder needs be named `*.bundle`.  In `*.bundle` put a `PackageContents.xml` file that contains the description of the appbundle and the relative path to its .addin file.

<pre>
&lt;?xml version="1.0" encoding="utf-8" ?&gt;
&lt;ApplicationPackage&gt;
  &lt;Components Description="Delete Walls"&gt;
    &lt;RuntimeRequirements OS="Win64" 
                         Platform="Revit" 
                         SeriesMin="R2018" 
                         SeriesMax="R2018" /&gt;
    &lt;ComponentEntry AppName="DeleteWalls" 
                    Version="1.0.0" 
                    ModuleName="./Contents/DeleteWalls.addin" 
                    AppDescription="Deletes walls" 
                    LoadOnCommandInvocation="False" 
                    LoadOnRevitStartup="True" /&gt;
  &lt;/Components&gt;
&lt;/ApplicationPackage&gt;
</pre>

Note: `SeriesMin` and `SeriesMax` both refer to Revit 2018 as `R2018`.  As of September 2018, Design Automation for Revit supports appbundles which run on Revit `R2018` and `R2019`.

In the `*.bundle\Contents` folder, put the `addin` file and the application `DLL` and its dependencies.  

<pre>
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;RevitAddIns&gt;
  &lt;AddIn Type="DBApplication"&gt;
    &lt;Name&gt;DeleteWalls&lt;/Name&gt;
    &lt;Assembly&gt;.\DeleteWalls.dll&lt;/Assembly&gt;
    &lt;AddInId&gt;d7fe1983-8f10-4983-98e2-c3cc332fc978&lt;/AddInId&gt;
    &lt;FullClassName&gt;DeleteWalls.DeleteWallsApp&lt;/FullClassName&gt;
    &lt;Description&gt;"Walls Deleter"&lt;/Description&gt;
    &lt;VendorId&gt;Autodesk&lt;/VendorId&gt;
    &lt;VendorDescription&gt;
    &lt;/VendorDescription&gt;
  &lt;/AddIn&gt;
&lt;/RevitAddIns&gt;
</pre>

Note: `Type` must be `DBApplication`.  Design Automation for Revit doesn't support applications that need Revit's UI functionality. 

`Assembly` must be a relative path to the `DLL`.

Examples of the format for the `*.bundle` folder and `PackageContent.xml` file can been found in the presentation on Autodesk Exchange Revit Apps [here](http://adndevblog.typepad.com/aec/ExchangeStorePublisher/3%20Autodesk%20Exchange%20Publish%20Revit%20Apps%20-%20Preparing%20Apps%20for%20the%20Store_Guidelines.pptx). While `PackageContents.xml` from existing Autodesk Exchange Revit apps can be used as-is, Design Automation for Revit only reads the `RuntimeRequirements` and `ComponentEntry` blocks which are circled in the image below.

## Publish an Appbundle

### APIs for app

This page explains the following `appbundle` APIs:

- Command &ndash; Endpoint URL &ndash; Description
- **POST** &ndash; **/v3/appbundles** &ndash;  [Creates a new app](#create-a-new-app).
- **POST** &ndash; **/v3/appbundles/{id}/aliases** &ndash;  [Creates a new alias for this app](#create-an-alias-for-the-app).
- **POST** &ndash; **/v3/appbundles/{id}/versions** &ndash;  [Creates a new version of the app](#create-a-new-version-number).
- **PATCH** &ndash; **/v3/appbundles/{id}/aliases/{aliasId}** &ndash;  [Modify alias details](#assign-an-existing-alias-to-another-version-of-an-app).



### Create a New App

To publish your appbundle to Design Automation, you need to POST your appbundle's identity and upload its package.

This example creates a new app `DeleteWallsApp` by posting its identity. The target engine of Revit running in Design Automation for this example app is Revit 2018. 

<pre>
curl -X POST \
  https://developer.api.autodesk.com/da/us-east/v3/appbundles \
  -H 'Authorization: Bearer LongStringAccessTokenObtainedDuringAuthenthication' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": "DeleteWallsApp",
  "engine": "Autodesk.Revit+2018",
  "description": "Delete Walls app based on Revit 2018"
}'
</pre>

- `id` &ndash;  The name given to the new app.
- `engine` &ndash;  The engine running in Design Automation used by the app.

#### Response

<pre>
{
  "uploadParameters": {
    "endpointURL": "https://dasdev-store.s3.amazonaws.com",
    "formData": {
      "key": "apps/Revit/DeleteWallsApp/1",
      "content-type": "application/octet-stream",
      "policy": "eyJleHBpcmF0aW9uIjoiMjAxOC... (truncated)",
      "success_action_status": "200",
      "success_action_redirect": null,
      "x-amz-signature": "6c68268e23ecb8452... (truncated)",
      "x-amz-credential": "ASIAQ2W... (truncated)",
      "x-amz-algorithm": "AWS4-HMAC-SHA256",
      "x-amz-date": "20180810... (truncated)",
      "x-amz-server-side-encryption": "AES256",
      "x-amz-security-token": "FQoGZXIvYXdzEPj//////////wEaDHavu... (truncated)"
    }
  },
  "engine": "Autodesk.Revit+2018",
  "description": "Delete Walls app based on Revit 2018",
  "version": 1,
  "id": "YourNickname.DeleteWallsApp"
}
</pre>

- `endpointURL` &ndash; This is the URL to which you must upload your app's ZIP file.
- `version` &ndash;  The version number for the app created by the POST request. For the Post request creating a new app, version always returns `1`.
- `formData` &ndash;  The form data that needs to be sent back when uploading the file in the following POST request.

### Upload app zip file

Now you can upload your app's ZIP to the signed URL returned by `endpointURL`:

<pre>
curl -X POST \
  https://dasdev-store.s3.amazonaws.com \
  -H 'Cache-Control: no-cache' \
  -F key=apps/Revit/DeleteWallsApp/1 \
  -F content-type=application/octet-stream \
  -F policy=eyJleHBpcmF0aW9uIjoiMjAxOC... (truncated) \
  -F success_action_status=200 \
  -F x-amz-signature=6c68268e23ecb8452... (truncated) \
  -F x-amz-credential=ASIAQ2W... (truncated) \
  -F x-amz-algorithm=AWS4-HMAC-SHA256 \
  -F x-amz-date=20180810... (truncated) \
  -F x-amz-server-side-encryption=AES256 \
  -F 'x-amz-security-token=FQoGZXIvYXdzEPj//////////wEaDHavu... (truncated)' \
  -F 'file=@path/to/your/app/zip'
</pre>

This is a `curl` example. You can use other way, e.g. Postman, to do the uploading. Just remember to include all form-data in your request.

### Create an Alias for the App

The new version of your app will be referenced via an [alias](Alias.md).  

This example creates an alias with id `test`. This alias labels version `1` of app `DeleteWallsApp`.

<pre>
curl -X POST \
  https://developer.api.autodesk.com/da/us-east/v3/appbundles/DeleteWallsApp/aliases \
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer LongStringAccessTokenObtainedDuringAuthenthication' \
  -d '{
      "version": 1,
      "id": "test"
    }'
</pre>


### Update an Existing App

### Create a New Version Number

To update an existing app, you need to create a new version for the app and then upload the updated zip package. 

If you still do the Post request for creating a new app [above](#create-a-new-app) , you will get a `409 Conflict` error.

This Post creates a new version for the app `DeleteWallsApp`.

<pre>
curl -X POST \
  https://developer.api.autodesk.com/da/us-east/v3/appbundles/DeleteWallsApp/versions\
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer LongStringAccessTokenObtainedDuringAuthenthication' \
  -d '{
      "id": null,
      "engine": "Autodesk.Revit+2018",
      "description": "Delete Walls app based on Revit 2018 Update"
    }'
</pre>


#### Response

<pre>
{
    "package": "https://dasprod-store.s3.amazonaws.com/appbundles/xxxxxxxx",
    "engine": "Autodesk.Revit+2018",
    "description": "Delete Walls app based on Revit 2018",
    "version": 2,
    "id": "YourNickname.DeleteWallsApp"
}
</pre>

The response to the app version post includes:

- `package` &ndash;  This is the signed URL to which you must upload your updated app package ZIP file.
- `version` &ndash;  The new version number for the app created by the above POST request.

Now you can upload the updated app's zip file to the new signed URL returned by `package` same as [above](#upload-app-zip-file).

### Assign an existing alias to another version of an app

You can update an existing alias to point to another version of an app.  

For example, after you post a new version of an app, you may wish to assign an existing alias to point to that new app's version.

Here is an example where alias `test` labels version `1` of an app `DeleteWallsApp`. A new version `2` has been posted for this app, but no alias labels version `2`:

- id       &ndash;     alias      &ndash;  version  
- DeleteWallsApp &ndash;  test &ndash;  1  
- DeleteWallsApp &ndash;  n/a    &ndash;   2 

You can reassign alias `test` to label app version `2`:
    
- id       &ndash;     alias      &ndash;  version  
- DeleteWallsApp &ndash;   n/a  &ndash;  1  
- DeleteWallsApp  &ndash;  test &ndash;   2 
    
To update the alias, you can either

* Delete the existing alias and recreate it with the version which you want to label.
* Do a PATCH request:

<pre>
curl -X PATCH \
  https://developer.api.autodesk.com/da/us-east/v3/appbundles/DeleteWallsApp/aliases/test \
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer LongStringAccessTokenObtainedDuringAuthenthication' \
  -d '{
      "version": 2
    }'
</pre>

## Engine Version Aliases

Each app POST request specifies the `engine` on which the application will run.  Different Design Automation engine version aliases correspond to different releases of Revit. The specified `engine` needs to be compatible with your app's PackageContent.xml `SeriesMin` and `SeriesMax`.

