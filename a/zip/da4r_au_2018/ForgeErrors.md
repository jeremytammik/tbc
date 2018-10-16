# Common Forge REST API Request errors

## Client id not whitelisted
```
Status: 403 Unauthorized
Body: {fault.name} - The client_id specified does not have access to the api product
```
If you see such an error, it is likely that the client id you used is not authorized to use the Design Automation for Revit's private beta service. Please [contact us](mailto:revit.io.support@autodesk.com) to get your client id whitelisted or use the one that is already whitelisted. Also, refer to our [Getting Started on Forge](Forge.md#creating-a-forge-app) documentation.

If you are already using a whitelisted client id and if you still see this error, please contact us at [revit.io.support@autodesk.com](mailto:revit.io.support@autodesk.com).


## Expired token
```
Status: 401 Unauthorized
Body: The token has expired or is invalid
```
This error can happen when the token attained to authenticate the Forge Application has expired. Please obtain a fresh token and perform the original request once again.

Please refer to the [link](Forge.md#authentication) to learn more.

## Invalid scope
```
Status: 403 Forbidden
Body: Token does not have the privilege for this request.
```
This error can happen when wrong scope is provided while authenticating the Forge Application. Please use `scope=code:all` to obtain a fresh token and perform the original request once again.

Please refer to the [link](Forge.md#authentication) to learn more.

## Too Many Requests
```
Status: 429 Too Many Requests
Body: You reached Quota limit. Your total free Quota is 20 requests per minute. Please try again soon.
```
This error can happen when more than allowed WorkItems are posted within a given minute. The current quota limit is 20 WorkItems per minute.

Please refer to the [link](QuotasAndRestrictions.md#workitem-limits) to learn more.

