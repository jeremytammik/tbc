<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- https://highlightjs.org/#usage
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
-->

<!-- https://prismjs.com -->
<link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>
<style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style>
</head>

<!---

- Licencing Plugins - What Revit subscriptions have to be considered?
  https://forums.autodesk.com/t5/revit-api-forum/licencing-plugins-what-revit-subscriptions-have-to-be-considered/m-p/12766353#M78805

- Entitlement Revit API - My update
  https://forums.autodesk.com/t5/revit-api-forum/entitlement-revit-api-my-update/td-p/12761235

twitter:

 the @AutodeskRevit #RevitAPI #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### RevitLookup Geometry Visualisation

Today we highlight two illuminating posts from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) on
licensing and the entitlement API:

####<a name="2"></a>

Julian Wandzilak of [W7k](https://w7k.pl/) shared
his [Entitlement Revit API update](https://forums.autodesk.com/t5/revit-api-forum/entitlement-revit-api-my-update/td-p/12761235):

> I am recently updating all my paid apps to 2025 - Thanks to this forum, everything goes rather smooth. Except I had some weird problems with my CheckOnline method which was based on provided method here: https://adndevblog.typepad.com/aec/2015/04/entitlement-api-for-revit-exchange-store-apps.html

> For some reason in 2025 I had some problems with RestSharp -> while checking the licence during OnApplicationInitialized method, the process was abruptly stopped, weirdly enough, without even throwing errors on me.

> Update helped but as RestSharp change I started having problems with older revit versions. I decided to rewrite it completely, but this time using default HttpClient:

<pre><code class="language-cs">public static bool CheckOnline( string appId, string userId )
{
    Uri uri = new Uri($"https://apps.exchange.autodesk.com/webservices/checkentitlement/?userid={userId}&appid={appId}");
    bool isValid = false;

    try
    {
        HttpClient myHttpClient = new HttpClient();

        Task&lt;HttpResponseMessage&gt; task = Task.Run(() =&gt; myHttpClient.GetAsync(uri));
        task.Wait();
        HttpResponseMessage response = task.Result;

        Task&lt;string&gt; readTask = response.Content.ReadAsStringAsync();
        string text = readTask.Result;

        EntitlementResponse entitlementResponse = JsonConvert.DeserializeObject&lt;EntitlementResponse&gt;(text);

        isValid = entitlementResponse.IsValid;
    }
    catch
    {
        return false;
    }

    return isValid;
}

[Serializable]
public class EntitlementResponse
{
    public string UserId { get; set; }
    public string AppId { get; set; }
    public bool IsValid { get; set; }
    public string Message { get; set; }
}

Here is the code. I hope it might be useful to someone. Works for me in 2020 - 2025
blog: w7k.pl more about me: Linkedin Profile
My add-ins for Revit: Drafter(180+ scripts) & Leveler


<center>
<img src="img/.jpg" alt="" title="" width="100"/>
</center>


