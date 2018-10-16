# Getting Started on Forge

## Creating a Forge App

The first step to using Design Automation for Revit is to [create a Forge app](https://developer.autodesk.com/en/docs/oauth/v2/tutorials/create-app).

The Forge app needs to be registered for the private beta.


## Authentication

Your Forge app requires authentication.

Please refer to
the [Forge OAuth documentation](https://developer.autodesk.com/en/docs/oauth/v2/overview/field-guide) for
more details.

You use the `Client ID` and `Client Secret` obtained above to authenticate your application and obtain a two-legged access token. The details of the HTTP response is given in the documentation above.  Please learn about the access tokens and their validity.

Please use `scope=code:all` instead of `scope=data:read` to obtain a token. All Design Automation for Revit APIs require this scope.

<pre>
curl -v 'https://developer.api.autodesk.com/authentication/v1/authenticate'
  -X 'POST'
  -H 'Content-Type: application/x-www-form-urlencoded'
  -d 'client_id=YourForgeAppClientID'
  -d 'client_secret=YourForgeAppClientSecret'
  -d 'grant_type=client_credentials'
  -d 'scope=code:all'
</pre>

The response body to this request contains the token that you use for all our APIs. The response also contains the expiration time of the token.
