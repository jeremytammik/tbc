<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- wv2
  https://autodesk.slack.com/archives/C0SR6NAP8/p1666290375135259
  Michael Dewberry, 20 Oct at 20:26
  Best practices for Revit add-ins using WebView2

 twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; Chaining Idling events and other solutions...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### WebView2

####<a name="2"></a> Future of CefSharp and WebView2 for Revit add-ins

An internal discussion aimed to define best practices for Revit add-ins using WebView2.

**Question:** I would like to make use of WaebView2 in my add-in.
What should I do?

- Rely on future versions of Revit installing the evergreen runtime?
- Use the same User Data Folder created by Revit for instances of the web view in add-ins?

For add-ins using WebView2 in Revit 2023 and earlier, before Revit itself includes it, is there a consensus on the best place to create a custom UDF?

My interpretation of the `CoreWebView2Environment.CreateAsync` docs are that all WebView2 invocations from the same process need to agree on the `options` object, but do not need to agree on the `userDataFolder`.

A sensible location for the UDF might be a subfolder of *%LOCALAPPDATA%\Autodesk\Revit\Autodesk Revit 2023*
&ndash; this is where Revit is currently creating the CEF cache folder &ndash;
but it's not clear if we should namespace that subfolder name with our add-in name or not. 

**Answer:** You only need to agree on the UDF if you want to use the same main WV2 process, i.e., act like a "tab" in the browser as opposed to starting a new browser "window". 
 The "tab" way &ndash; sharing the UDF and options &ndash; is more performant and uses less memory.

In that case, you need to use the same options as all the other instances.
If you use your own UDF, you are free to set any options you like.

However, we are currently finding WV2 unstable with Revit and may choose not to use it, in which case we will continue using CEF.

<!--

We are seeing what looks like crashes of the rendering subprocesses in some situations like opening certain large files... memory issues? Blocking the main thread for too long?
We don't know yet.
It also crashes after leaving Revit idle overnight.

-->

The worst part is that if one instance of WV2 crashes, all the other instances stop working, even newly created ones after that.
The only remedy is to restart Revit.
Other products' experience with WV2 seems to be fine and I haven't been able to reproduce these issues outside of Revit.
You might want to put your add-in through some tests.

<!--

Actually, if it works for you without problems, we should probably compare notes how we are using and setting up WV2.

-->

In Revit, we are working on a separate component that isolates the browser code from Revit and I would like all our uses to go through that.
It supports both CEF and WV2, so it's easy to switch which one we want to use.
Among other things, it takes care of setting up the UDF so they are all in the same location.

The development builds of Revit include both WV2 and the evergreen runtime.
In fact, it was put into Revit even before we started the migration; looks like the SSO component uses it now.

Dynamo is currently fully committed to WebView2 from v2.17, so we are testing its stability there as well.

**Response:** If there is a push to revert back to cefsharp, does this mean future releases won't have wv2?

 **Answer:** No, I think we will still be shipping both CefSharp and WV2 for some time, since our component needs both even if we're using just one of them.
 We will try to switch again once WV2 is more stable or once we have more time to figure out what's going on.
If anything, we might stop shipping CefSharp at some point. 

Also, WV2 comes included in Win11 by default and can't be uninstalled (I think...), so that will be no issue hopefully soon.



####<a name="3"></a> 

####<a name="4"></a> 

####<a name="5"></a> 

**Question:** 


**Answer:** 

**Response:** 

<pre class="code">

</pre>




<center>
<img src="img/.png" alt="" title="" width="100" height=""/> <!-- 960 x 928 -->
</center>
