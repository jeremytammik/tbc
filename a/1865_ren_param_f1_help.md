<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/insidefactoryama


linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Rename Shared Param 


####<a name="2"></a> ContextualHelp Issue

A colleague lookd into an issue with `ContextualHelp` and wrote the following based on own tests and the info in the development ticket:

Using `ContextualHelp` you can provide a URL for any button that should be shown when the user clicks `F1` while a ribbon button tooltip is displayed.

If the URL has a space character in the path, you usually encode it either as `%20` or `+`.

That may cause an issue for `ContextualHelp`.

Let’s use this URL as an example:

- `postman-echo.com/get?text%20with%20space`

<center>
<img src="img/an_f1_help_1.png" alt="Contextual help test URL" title="Contextual help test URL" width="600"/> <!-- 896 -->
</center>

First problem: if you use `https`, then *https://accounts.autodesk.com/oAuth/OAuthRedirect* will be called with a redirect to the actual URL you specified for `F1` using the `ContextualHelp` object.

This will confuse and stall users.

Secondly: if, on top of that, the URL also contains a space (`%20` or `+`), then you’ll get stuck on that webpage:

- https://accounts.autodesk.com/oAuth/OAuthRedirect?oauth_consumer_key=1c27193f-af5e-4e7c-9847-06cd5c3c30ae&oauth_nonce=cd819e65f0ac476099e9c795a22c05a7&oauth_redirect_url=https%3A%2F%2Fpostman-echo.com%2Fget%3Ftext%2520with%2520space&oauth_signature=xl7aBEcj5lI%2FX28ozkvQ%2Ba163qg%3D&oauth_signature_method=HMAC-SHA1&oauth_timestamp=1600289858&oauth_token=bskZ8nJbcvBt%2FTyQvS%2FeImjP6pc%3D&oauth_version=1.0

It displays the following error message:

- xoauth_problem=parameter_rejected&xoauth_parameters_absent=oauth_redirect_url&oauth_error_message=Invalid%20value%20for%20parameter%3Aoauth_redirect_url

<center>
<img src="img/an_f1_help_1.png" alt="Contextual help test redirect" title="Contextual help test redirect" width="1200"/> <!-- 2424 -->
</center>

You can reproduce this issue with any sample using the following code:

<pre class="code">
  ContextualHelp contextualHelp = new ContextualHelp(
    ContextualHelpType.Url,
    "https://postman-echo.com/get?text%20with%20space" );

  pushButton.SetContextualHelp(contextualHelp);
</pre>

Both of the above-mentioned problems can be avoided by passing the link as `http` instead of `https`.

If the given website redirects from `http` to `https` that won’t cause a problem.

<pre class="code">
  ContextualHelp contextualHelp = new ContextualHelp(
    ContextualHelpType.Url,
    "http://postman-echo.com/get?text%20with%20space" );

  pushButton.SetContextualHelp(contextualHelp);
</pre>

I hope you find this useful as well.

