<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---


#dotnet #csharp
#fsharp #python
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #rendering
#3dweb #3dviewAPI #html5 #threejs #webgl #3d #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restAPI
#mongoosejs #mongodb #nodejs
#rtceur
#xaml
#3dweb #a360 #3dwebaccel #webgl @adskForge
@AutodeskReCap @Adsk3dsMax
#revitAPI #bim #aec #3dwebcoder #adsk #adskdevnetwrk @jimquanci @keanw
#au2015 #rtceur
#eraofconnection
#RMS @researchdigisus
@adskForge #3dwebaccel
#a360
@github

Revit API, Jeremy Tammik, akn_include

Entitlement API Trial Period and Floating License #revitAPI #3dwebcoder @AutodeskRevit #bim #aec #adsk #grevit

I had an exciting weekend with ski tours under challenging weather conditions around Oberalppass, Sedrun-Andermatt, skiing on Piz Maler on Friday, piste in a snow storm on Saturday, and climbing Piz Cavradi via the Maighelshuette Sunday morning in splendid conditions, with 50 cm of fresh powder snow for the descent.
Here is today's Revit news for you
&ndash; Trial period, floating license and the Entitlement API
&ndash; SketchUp Plugin for Grevit
&ndash; EMEA ADN team meeting in London...

-->

### Entitlement API, Trial Period and Floating License

I had an exciting weekend with ski tours under challenging weather conditions around Oberalppass, Sedrun-Andermatt, skiing on Piz Maler on Friday, piste in a snow storm on Saturday, and climbing Piz Cavradi via the Maighelshuette Sunday morning in splendid conditions, with 50 cm of fresh powder snow for the descent
([photo album](https://www.facebook.com/media/set/?set=a.10207632719508939&type=1&l=42a28d5c14)).

Here is today's Revit news for you:

- [Trial period, floating license and the Entitlement API](#2)
- [SketchUp Plugin for Grevit](#3)
- [EMEA ADN team meeting in London](#4)

#### <a name="2"></a>Trial Period, Floating License and the Entitlement API

Here is a question that came up in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160)
on [how to set up a trial period for a custom add-in](http://forums.autodesk.com/t5/revit-api/dynamic-model-update-after-loading-family/m-p/6052310),
answered by my
colleagues [Cyrille Fauvel](http://adndevblog.typepad.com/cloud_and_mobile/cyrille-fauvel.html)
and [Virupaksha Aithal](http://adndevblog.typepad.com/autocad/virupaksha-aithal.html):


**Question:** I have an external application Revit add-in that I want to set a trial period on and sell on Autodesk AppStore, formerly known as Autodesk Exchange Apps.

My first question is:

Where should I store the trial period information?

I tried setting the `RegistryKey` on `Registry.LocalMachine` but Revit does not allow that because of the security problem.

My second question is about the Autodesk Entitlement API service:

Is it possible for a user to use one single permission account on multiple computers and pass the Entitlement API at the same time?

**Answer:** On the first question, where to store the trial period information:

That is completely up to you.

For instance, you can store it in your remote website database, in a local encrypted file, or anywhere else you like.

On the second question about the Autodesk Entitlement API service:

Yes, the entitlement API is a REST call to check whether the license is valid. It does not track the usage.
You would have to implement your own service to track usage.

I assume that the scenario you wish to avoid is one single user, John, running the app from several different computers at the same time.
To prevent that, you can use a floating license schema, like this:

John starts the app on computer A:

- You check the entitlement.
- You log a record on your website noting that computer A is currently using the license, e.g. storing its IP address, the time stamp, the license, etc.
- Every N minutes, refresh the record on your website.
    - Lock the app if the Internet connection is lost.
- When John shuts down the app, inform your website and release the 'currently using' record for computer A.

If John starts the app on computer B, you proceed similarly, but:

- If your website says 'license already in use', give a grace period of N minutes.
- N minutes later:
    - If the record for computer A gets updated, lock the app on Computer B.
    - If the record for computer A does not get updated for some reason, e.g. crashed, give another N minutes grace period.
- N minutes later still: if there is still no change, take the license, and revoke it for computer A, which will lock down from now on.

If more than two computers are playing in this game, you may decide on stronger rules, set a priority on who can take the license, etc.

So, you need to implement your own solution to handle simultaneous use of the app on multiple machines.

For information on the current implantation entitlement API implementation, please refer to the following material &ndash; it is general in nature and thus holds good for Revit also:

- [Entitlement API recording](https://autodesk.box.com/s/aj41r4y9inou6hh3mps2ri8stz9nelt1)
- [Sample application source code](https://github.com/ADN-DevTech/EntitlementAPI)
- [Implementing copy protection in your apps](http://usa.autodesk.com/adsk/servlet/item?siteID=123112&id=24243607)
- [Entitlement API for desktop apps](http://usa.autodesk.com/adsk/servlet/item?siteID=123112&id=24243865)

I mentioned some additional resources in the recent discussion of the [Autodesk reorganisation and Kean's move to Octo](http://thebuildingcoder.typepad.com/blog/2016/02/reorg-fomt-devcon-ted-qr-custom-exporter-quality.html#2):

- [Entitlement API for Revit Exchange Store Apps](http://adndevblog.typepad.com/aec/2015/04/entitlement-api-for-revit-exchange-store-apps.html)
- [Exchange Apps resources &ndash; Entitlement API information](http://thebuildingcoder.typepad.com/blog/2014/05/exchange-apps-webinar-recording-and-resources.html#3)
- [Securing your AutoCAD app using .NET](http://through-the-interface.typepad.com/through_the_interface/2016/02/securing-your-autocad-app-using-net.html) &ndash; Kean's sample showing how to use a REST API to check the user entitlement for your application via the Autodesk AppStore.

I hope that this helps and provides a final, complete and comprehensive overview.



#### <a name="3"></a>SketchUp Plugin for Grevit

Last year,
I [mentioned](http://thebuildingcoder.typepad.com/blog/2015/07/grevit-firerating-in-the-cloud-demo-deployment-vacation.html#3) the
exciting open source project [Grevit](http://grevit.net),
a [Rhino](http://www.rhino3d.com) and [Grasshopper](http://www.grasshopper3d.com) app
that enables assembling a BIM in Grasshopper, sending it to Revit or AutoCAD Architecture, and later dynamically updating it.

Its author, Max Thumfart, Senior Engineer at [Thornton Tomasetti](http://www.thorntontomasetti.com) in
the UK, points out that the latest Grevit release also adds support
for [SketchUp](http://www.sketchup.com).

You can now build your BIM Model in either Grasshopper or SketchUp and send it to Revit or ACA, where it is translated into native elements like walls, floors and columns.

<center>
<img src="img/grevit.png" alt="Grevit" width="464">
</center>


#### <a name="4"></a>EMEA ADN Team Meeting in London

I am travelling to London tomorrow, for a short team meeting with my closest colleagues, the European ADN team.

We will discuss many exciting things, our internal organisation, efficiency, meetups, webinars, developing material and samples, best practices, individual achievements, upcoming events, other plans for the coming year, and last but not least all the APIs we support: Manufacturing, Fusion, BIM, BIM360 Docs, the View and Data API, AutoCAD I/O, ReCap, Spark, EXPO, Shotgun, Stingray, Maya I/O, 3ds Max I/O, Rendering, etc. &ndash; all handled by just four people in this part of the world!
