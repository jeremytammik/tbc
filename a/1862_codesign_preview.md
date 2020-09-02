<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Revit 2018 Code Signing
  https://forums.autodesk.com/t5/revit-api-forum/revit-2018-code-signing/m-p/9715700#M49301
  Peter Hirn's yaml file for RevitLookup helps solve
 
- preview_control_rotates_model.mp4
  Used PreviewControl ElementHost and PickPoint
  https://forums.autodesk.com/t5/revit-api-forum/used-previewcontrol-elementhost-amp-pickpoint/m-p/9715680

- cool checks for element type
  Determine if element is ElementType
  https://forums.autodesk.com/t5/revit-api-forum/determine-if-element-is-elementtype/m-p/9713330#M49253

- "Aligning AI With Shared Human Values" https://arxiv.org/pdf/2008.02275v1.pdf

twitter:

Revit add-in code signing, preview control rotates model and element type predicates for the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/previewcodesign

A few interesting threads from the Revit API discussion forum and AI news
&ndash; Revit add-in code signing YAML
&ndash; Preview control rotates model
&ndash; Element type predicates
&ndash; AI ethics...

linkedin:

Revit add-in code signing, preview control rotates model and element type predicates for the #RevitAPI

http://bit.ly/previewcodesign

A few interesting threads from the Revit API discussion forum and AI news:

- Revit add-in code signing YAML
- Preview control rotates model
- Element type predicates
- AI ethics...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Preview, Code Signing and Element Type Predicates

Picking up a few interesting threads from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) and AI news:

- [Revit add-in code signing YAML](#2)
- [Preview control rotates model](#3)
- [Element type predicates](#4)
- [AI ethics](#5)

<center>
<img src="img/scale_weight_heart_versus_feather_of_truth_in_book_of_the_dead.jpg" alt="Weight of a heart versus feather of truth in Book of the Dead" title="Weight of a heart versus feather of truth in Book of the Dead" width="600"/>
<p style="font-size: 80%; font-style:italic">Weight of a heart versus feather of truth in Book of the Dead</p>
</center>



####<a name="2"></a>Revit Add-In Code Signing YAML

Peter Hirn's [YAML file for automating the code signing](https://thebuildingcoder.typepad.com/blog/2020/08/revitlookup-continuous-integration-and-graphql.html#3) of
the [continuous integration build of RevitLookup](https://thebuildingcoder.typepad.com/blog/2020/08/revitlookup-continuous-integration-and-graphql.html#2) helps solve
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Revit 2018 Code Signing](https://forums.autodesk.com/t5/revit-api-forum/revit-2018-code-signing/m-p/9715700):

**Question:** I am attempting to code sign my Revit add-in and I cannot seem to get Revit to recognise it.
I have a self-signed certificate which I have manually installed into my trusted root authorities, I have built out the add-in in Visual Studio and copied all DLL's and addin file to the addins folder. I have then run the sign tool command:

<pre class="code">
  signtool sign /f "Path/To/codesign.pfx" /t \
    http://timestamp.comodoca.com/authenticode \
    /p "MyPassword" "Path/To/Addins//2018/AppName.dll"
</pre>

This command executes successfully and I can see a digital signatures tab containing the information I added for my self-signed certificate in the DLL properties.
Yet, When I start Revit, it still says it is unsigned...
Where did I go wrong?

I have tried various other combinations of this command including passing in the .addin file which errors out saying unrecognized file and signing all dlls in my addins folder.

**Answer:** Peter Hirn added an automatic signing step to the [RevitLookup continuous integration on GitLab](https://thebuildingcoder.typepad.com/blog/2020/08/revitlookup-continuous-integration-and-graphql.html#2).

The YAML file executing the add-in signing is available in the [RevitLookup GitHub repository](https://github.com/jeremytammik/RevitLookup).

The pertinent bits are
the [lines 53-79 in `.gitlab-ci.yml`](https://github.com/jeremytammik/RevitLookup/blob/master/.gitlab-ci.yml#L53-L79):

<pre class="prettyprint">
  script:
    - apt-get update && apt-get install -y --no-install-recommends osslsigncode
    - mkdir -p .secrets && mount -t ramfs -o size=1M ramfs .secrets/
    - echo "${CODESIGN_CERTIFICATE}" | base64 --decode > .secrets/authenticode.spc
    - echo "${CODESIGN_KEY}" | base64 --decode > .secrets/authenticode.key
    - osslsigncode -h sha1 -spc .secrets/authenticode.spc -key .secrets/authenticode.key -t ${TIMESERVER} -in "${ASSEMBLY}" -out "${ASSEMBLY}-sha1"
    - osslsigncode -nest -h sha2 -spc .secrets/authenticode.spc -key .secrets/authenticode.key -t ${TIMESERVER} -in "${ASSEMBLY}-sha1" -out "${ASSEMBLY}"
    - rm "${ASSEMBLY}-sha1"
</pre>

**Response:** This is great!
I am going to utilize that in my own Repos.
Ok, I finally got windows to recognize my cert;
Let me track through what I did and how I got it to work.

Originally, I was using `openssl` on a linux server to generate the certs and private keys.
I then used `openssl` to convert those to a `pfx` file and was using that to sign stuff;
Looks like that does not work too well.

I followed the post in this thread and that worked...

To rehash what I did to make this work:

Notes before you begin: `MakeCert.exe`, `pvk2pfx.exe`, and `signtool.exe` are all located in *C:\Program Files (x86)\Windows Kits\8.1\bin\x64\*.
I have already added this to my environment variables; make sure you do so too, otherwise you will have to specify in your command where to find these exes.

<pre class="code">
MakeCert.exe -r -sv AVT_CodeSign.pvk -n "CN=AVT" AVTCodeSign.cer -b 01/01/2020 -e 12/31/2020
</pre>

NOTE: MakeCert is deprecated according to official Microsoft sources, but at the time of this writing that command still works.
They have an alternative PowerShell commandlet that does the same thing though I did not try that since MakeCert worked for me.

<pre class="code">
pvk2pfx.exe -pvk codesign.pvk -pi MySecurePassword -spc codesign.cer -pfx codesign.pfx -po MyOtherSecurePassword
</pre>

NOTE: The passwords can be the same here; The password I'm using here is from a prompt from Step 1.
That prompt will ask you to supply a password for the private key or give you the option to press "none".
Whichever you choose, supply that password in step 2.

Step 3: Use `CertMgr.msc` to install your `codesign.pfx` file NOT the certificate.
Install it Trusted Publishers, then in Trusted Root Certification Authorities

NOTE: It's important here to click your `.pfx` file as when I did this step, the default was choosing the cert file.
I'm unsure if this way is better than simply right clicking and hitting "install cert" but it seems both will get you to the same spot.

<pre class="code">
signtool sign /f "Path\To\codesign.pfx" /t http://timestamp.verisign.com/scripts/timstamp.dll /p "MySecurePassword" "Path\To\RevitAddin.dll"
</pre>

Following this process, I was able to get a valid digital signature.
I'm not sure why generating it using `openssl` on linux was causing an issue but there may be something to this method that works better.

Many thanks to ShinyKey for testing and confirming this, and thanks again to Peter Hirn for the smooth RevitLookup CI and code signing workflow.

####<a name="3"></a>Preview Control Rotates Model

Scott Ehrenworth of [Microdesk Inc.](https://www.microdesk.com) pointed out a few interesting aspects of using and interacting with the Revit API [PreviewControl class](https://www.revitapidocs.com/2020/50112279-5c9d-0351-bbd1-698e76be9e36.htm) in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [using PreviewControl ElementHost and PickPoint]( https://forums.autodesk.com/t5/revit-api-forum/used-previewcontrol-elementhost-amp-pickpoint/m-p/9715680):

Firstly, The `PreviewControl` is <u>not</u> a pure preview control, providing read-only access and no user interaction with the model.

Proof: When changing the orientation or rotation of a 3D view inside of a `PreviewControl` window, the model's 3D view will rotate as well:

<center>
<video style="display:block; width:600px; height:auto;" autoplay="" muted="" loop="loop">
<source src="img/preview_control_rotates_model.mp4" type="video/mp4">
<source src="https://thebuildingcoder.typepad.com/2020/banana_small.mp4" type="video/mp4">
</video>
</center>

Zoom and Pan do not have this effect.

Secondly, even though the preview control modifies the model's current view settings, it does not require a transaction to do so.
However, it does require the manual transaction option, presumably so that it can start and commit its own transaction internally.

This could be like the `PromptForFamilyInstancePlacement` method that has a transaction control built in.
The PreviewControl class is full of Event Handlers, so maybe this is the case?

Here's the very basic sample of the external command I used to test this for 3D views.

<pre class="code">
&nbsp;&nbsp;[<span style="color:#2b91af;">Transaction</span>(&nbsp;<span style="color:#2b91af;">TransactionMode</span>.Manual&nbsp;)]
&nbsp;&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">PreviewControlTest</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalCommand</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;uiapp&nbsp;=&nbsp;commandData.Application;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;=&nbsp;uiapp.ActiveUIDocument;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WPF_GridContainer&nbsp;newGrid&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;WPF_GridContainer();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newGrid.theGrid.Children.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">PreviewControl</span>(&nbsp;doc,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">View3D</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FirstElementId()&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newGrid.ShowDialog();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

I assume it will refuse to run in read-only transaction mode, then...

Yes, I can confirm:

Switching the `TransactionMode` to `ReadOnly` is now just hanging the command (never opens the window for the PreviewControl).

Funky stuff, but good to know!

Many thanks to Scott for this useful information and research.

For pointers to several previous discussions of various aspects of the `PreviewControl`, check out the post
on [zooming in a preview control](https://thebuildingcoder.typepad.com/blog/2013/09/appstore-advice-and-zooming-in-a-preview-control.html#4).

####<a name="4"></a>Element Type Predicates

Here are a bunch of ways to check and filter for Revit element types making use of both .NET generics and the Revit API filtering functionality suggested in the question on how 
to [determine if element is `ElementType`](https://forums.autodesk.com/t5/revit-api-forum/determine-if-element-is-elementtype/m-p/9713330):

**Question:** I'm listening to the `Application` `DocumentChanged` event and would like to see if the element modified is an Instance or an ElementType.
I'm currently doing the following but it doesn't quite satisfy all the conditions:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;IsElementType(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;element&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;typeId&nbsp;=&nbsp;element?.GetTypeId();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;typeId&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;||&nbsp;typeId&nbsp;==&nbsp;<span style="color:#2b91af;">ElementId</span>.InvalidElementId&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!(element&nbsp;<span style="color:blue;">is</span>&nbsp;Room)&nbsp;&amp;&amp;&nbsp;!(element&nbsp;<span style="color:blue;">is</span>&nbsp;PipeSegment)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;}
</pre>

I'm not native to Revit sorry if my terminology is off.
How does the `FilterCollector.WhereElementIsElementType()` determine it's an ElementType?

**Answer:** This will return false if type is `ElementType` but true if is derived from `ElementType`:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;Bl&nbsp;=&nbsp;element.GetType().IsSubclassOf(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">ElementType</span>&nbsp;)&nbsp;);
</pre>

This will return true if type is ElementType or is derived from ElementType:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;B2&nbsp;=&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">ElementType</span>&nbsp;).IsAssignableFrom(&nbsp;element.GetType()&nbsp;);
</pre>

Here's a more Revit orientated approach in VB: you asked how a collector determines an ElementType, but you can always pass a list of a single element into a collector and filter that:

<pre class="code">
  Dim FEC As New FilteredElementCollector(element.Document, {element.Id}.ToList)
  Dim B3 As Boolean = FEC.WhereElementIsElementType.ToElementIds.Count > 0
</pre>

You can't do it in C# in this abbreviated fashion due to the need to use `ICollection<T>` directly (option `Strict` `off` in VB), but you can do similar.

**Answer 2:** Here's a simpler way of putting it:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;IsElementType(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;element&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;element&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">ElementType</span>;
&nbsp;&nbsp;}
</pre>

I'd suggest not even making a method for this and just using the `is` expression.

**Answer 3:** I use the `is` operator quite often in my work.

Here is an online method which will use a [type pattern](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/is#type-pattern) to give you the `EType` as a variable:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;element&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">ElementType</span>&nbsp;EType&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;Type&nbsp;Name&quot;</span>,&nbsp;EType.Name&nbsp;);
&nbsp;&nbsp;}
</pre>

Interesting to note that in VB the `is` statement only returns true if the exact same type is compared.
Since `is` is used to check if two things are the same object instance.
The equivalent to the way it is used in C# seems to be:

<pre class="code">
  Dim B1 As Boolean = TypeOf Element Is ElementType
</pre>

####<a name="5"></a>AI Ethics

A very interesting and promising first stab at creating 'good' AI and NLP that conforms with common human ethical judgement and behaviour is presented in the research paper 
on [Aligning AI With Shared Human Values](https://arxiv.org/pdf/2008.02275v1.pdf).

