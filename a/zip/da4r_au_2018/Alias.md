# Alias

An `alias` is a label to a specific version of `app` or `activity`. Aliasing gives you more control over how you update appbundles and activities.

For example, versions of an app `DeleteWallsApp` may be referenced by aliases `test` and `prod`.

id       |    alias      | version 
------------- |:-------------:| :-----:      
DeleteWallsApp | prod | 1    
DeleteWallsApp |  | 2 
DeleteWallsApp |  | 3
DeleteWallsApp |  test  | 4

The alias `prod` refers to version `1` of the `DeleteWallsApp` app, which could be a stable version of your app.
The alias `test` refers to version `4` of the `DeleteWallsApp` app, which could be the latest version you are debugging.


Here is an example activity `DeleteWallsActivity` whose versions are referenced by aliases `test` and `prod`.

 id       |    alias      | version  
 ----- |:-----:| :------:
 DeleteWallsActivity | prod | 1   
 DeleteWallsActivity |  | 2 
 DeleteWallsActivity  |  test   |  3 
 
The alias `prod` refers to version `1` of the `DeleteWallsActivity` activity, which could be a stable version of your activity.
The alias `test` refers to version `3` of the `DeleteWallsActivity` activity, which could be the latest version you are still testing.


## Using Aliases
 
In Design Automation, workitems uses aliases to reference activities, and activities use aliases to reference appbundles. 

```sh
DeleteWallsWorkitem
 ---> DeleteWallsActivity
       ---> DeleteWallsApp
``` 

To reference a specific version of app or activity, build a string in the format **"`owner`.`id`+`alias`"** for this version of app or activity.  

Parameter | Description
--------|-----------
`owner` | The nickname for your Forge Client App id.
`id` | The `app` or `activity` id.
`alias` | The alias id labeling the specific version.

For example, "`YourNickname.DeleteWallsApp+test`" would reference version `2` of the app `DeleteWallsApp`.

id       |    alias      | version 
------------- |:-------------:| :-----:      
DeleteWallsApp | prod | 1    
DeleteWallsApp | test | 2 
DeleteWallsApp |    | 3
DeleteWallsApp |    | 4

then your activity can reference version `2` of the app `DeleteWallsApp` by the alias `test`.
For example, `YourNickname.DeleteWallsActivity+test` references `YourNickname.DeleteWallsApp+test`

If you later reassign the alias `test` to the latest version `4` of `DeleteWallsApp`, then the activity `YourNickname.DeleteWallsActivity+test` will automatically run against version `4` of `DeleteWallsApp` without any changes to the definition of this activity.


## Create an Alias for an app/activity

* This example creates an alias with id `test`. The alias labels version `2` of app `DeleteWallsApp`.

  ```sh
  curl -X POST \ 
    https://developer.api.autodesk.com/da/us-east/v3/appbundles/DeleteWallsApp/aliases \
    -H 'Content-Type: application/json' \
    -H 'Authorization: Bearer LongStringAccessTokenObtainedDuringAuthenthication' \
    -d '{
        "version": 2,
        "id": "test"
      }'
  ```
  Notes: `https://developer.api.autodesk.com/da/us-east/v3/appbundles/{appId}/aliases` - The {appId} can be changed to use this example with other app's id. 

* This example creates an alias with id `test`. The alias labels version `2` of activity `DeleteWallsActivity`.

  ```sh
  curl -X POST \ 
    https://developer.api.autodesk.com/da/us-east/v3/activities/DeleteWallsActivity/aliases \
    -H 'Content-Type: application/json' \
    -H 'Authorization: Bearer LongStringAccessTokenObtainedDuringAuthenthication' \
    -d '{
        "version": 2,
        "id": "test"
      }'
  ```
  Notes: `https://developer.api.autodesk.com/da/us-east/v3/activities/{activityId}/aliases` - The {activityId} can be changed to use this example with other activity's id. 
    

## Update an Alias for an app/activity

* This example uses PATCH to reassign the alias `test` to version `4` of the app `DeleteWallsApp`.

  ```sh
  curl -X PATCH \
    https://developer.api.autodesk.com/da/us-east/v3/appbundles/DeleteWallsApp/aliases/test \
    -H 'Content-Type: application/json' \
    -H 'Authorization: Bearer LongStringAccessTokenObtainedDuringAuthenthication' \
    -d '{
        "version": 4
      }'
  ```

  Notes:
  * `https://developer.api.autodesk.com/da/us-east/v3/appbundles/{appId}/aliases/{aliasId}` - The {appId} and {aliasId} can be changed to use this example with other app's id and alias's id.
  * `version` - The version number of the app the alias will label.


* This example uses PATCH to reassign the alias `test` to version `3` of the activity `DeleteWallsActivity`.

  ```sh
  curl -X PATCH \
    https://developer.api.autodesk.com/da/us-east/v3/activities/DeleteWallsActivity/aliases/test \
    -H 'Content-Type: application/json' \
    -H 'Authorization: Bearer LongStringAccessTokenObtainedDuringAuthenthication' \
    -d '{
        "version": 3
      }'
  ```

  Notes:
  * `https://developer.api.autodesk.com/da/us-east/v3/activities/{activityId}/aliases/{aliasId}` - The {activityId} and {aliasId} can be changed to use this example with other activity's id and alias's id. 
  * `version` - The version number of the activity the alias will label.


## References

* The APIs for `app` can be found [here](http://v3doc.s3-website-us-west-2.amazonaws.com/#/AppsApi).
 
* The APIs for `activity` can be found [here](http://v3doc.s3-website-us-west-2.amazonaws.com/#/ActivitiesApi).

* If you get an error, please refer to some common errors listed [here](ForgeErrors.md).
