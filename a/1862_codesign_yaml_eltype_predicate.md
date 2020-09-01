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

- super cool checks for element type
  Determine if element is ElementType
  https://forums.autodesk.com/t5/revit-api-forum/determine-if-element-is-elementtype/m-p/9713330#M49253

- "Aligning AI With Shared Human Values" https://arxiv.org/pdf/2008.02275v1.pdf

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Add-In Code Signing and Element Type Predicates

#### <a name="2"></a>Revit Add-In Code Signing YAML

Peter Hirn's [YAML file for automating the code signing](https://thebuildingcoder.typepad.com/blog/2020/08/revitlookup-continuous-integration-and-graphql.html#3) of
the [continuous integration build of RevitLookup}(https://thebuildingcoder.typepad.com/blog/2020/08/revitlookup-continuous-integration-and-graphql.html#2) helps solve
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Revit 2018 Code Signing](https://forums.autodesk.com/t5/revit-api-forum/revit-2018-code-signing/m-p/9715700):

**Question:** I am attempting to code sign my Revit add-in and I can not seem to get Revit to recognize it.
I have a self-signed certificate which I have manually installed into my trusted root authorities, I have built out the add-in in Visual Studio and copied all DLL's and addin file to the addins folder. I have then run the sign tool command:

<pre class="code">
  signtool sign /f "Path/To/codesign.pfx" /t \
    http://timestamp.comodoca.com/authenticode \
    /p "MyPassword" "Path/To/Addins//2018/AppName.dll"
</pre>

This command executes successfully and I can see a digital signatures tab containing the information I added for my self signed certificate in the DLL properties.
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
They have an alternative Powershell commandlet that does the same thing though I did not try that since MakeCert worked for me.

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

Following this process I was able to get a valid digital signature.
I'm not sure why generating it using `openssl` on linux was causing an issue but there may be something to this method that works better.

Many thanks to ShinyKey for testing and confirming this, and thanks again to Peter Hirn for the smooth RevitLookup CI and code signing workflow.

####<a name="3"></a>Preview Control Rotates Model

Scott Ehrenworth of [Microdesk Inc.](https://www.microdesk.com) poited out a few interesting aspects of using and interacting with the Revit API [PreviewControl class](https://www.revitapidocs.com/2020/50112279-5c9d-0351-bbd1-698e76be9e36.htm) in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Using PreviewControl ElementHost and PickPoint]( https://forums.autodesk.com/t5/revit-api-forum/used-previewcontrol-elementhost-amp-pickpoint/m-p/9715680):

- The `PreviewControl` is not a pure preview control, providing read-only access and no user interaction with the model.
Proof:
When changing the orientation or rotation of a 3D view inside of a `PreviewControl` window, the model's 3D view will rotate as well:

<center>
<video style="display:block; width:600px; height:auto;" autoplay="" muted="" loop="loop">
<source src="img/preview_control_rotates_model.mp4" type="video/mp4">
<source src="https://thebuildingcoder.typepad.com/2020/banana_small.mp4" type="video/mp4">
</video>
</center>

Zoom and Pan do not have this effect.

 

 


For pointers to several previous discussions of various aspects of the `PreviewControl`, check out the post
on [zooming in a preview control](https://thebuildingcoder.typepad.com/blog/2013/09/appstore-advice-and-zooming-in-a-preview-control.html#4).

- preview_control_rotates_model.mp4

####<a name="3"></a>Element Type Predicates

Here are a bunch of ways to check and filter for Revit element types making clever use of both .NET generics and the Revit API filtering functionality brought up in the question on how 
to [determine if element is `ElementType`](https://forums.autodesk.com/t5/revit-api-forum/determine-if-element-is-elementtype/m-p/9713330):

**Question:** I'm listening to the `Application` `DocumentChanged` event and would like to see if the element modified is an Instance or an ElementType.
I'm currently doing the following but it doesn't quite satisfy all the conditions:

<pre class="code">
static bool IsElementType(Element element)
{
    var typeId = element?.GetTypeId();
    if (typeId == null || typeId == ElementId.InvalidElementId)
    {
        if (!(element is Room) && !(element is PipeSegment))
        {
            return true;
        }
    }    
    return false;
}
</pre>

I'm not native to Revit sorry if my terminology is off.
How does the `FilterCollector.WhereElementIsElementType()` determine it's an ElementType?

**Answer:** This will return false if type is `ElementType` but true if is derived from `ElementType`:

<pre class="code">
  bool Bl = element.GetType().IsSubclassOf(typeof(ElementType));
</pre>

This will return true if type is ElementType or is derived from ElementType:

<pre class="code">
  bool B2 = typeof(ElementType).IsAssignableFrom(element.GetType());
</pre>

Here's a more Revit orientated approach in VB: you asked how a collector determines an ElementType, but you can always pass a list of a single element into a collector and filter that:

<pre class="code">
  Dim FEC As New FilteredElementCollector(element.Document, {element.Id}.ToList)
  Dim B3 As Boolean = FEC.WhereElementIsElementType.ToElementIds.Count > 0
</pre>

You can't do it in C# in this abbreviated fashion due to the need to use `ICollection&lt;T&gt;` directly (option `Strict` `off` in VB), but you can do similar.

**Answer 2:** Here's a simpler way of putting it:

<pre class="code">
  static bool IsElementType(Element element)
  {
    return element is ElementType;
  }
</pre>

I'd suggest not even making a method for this and just using the `is` expression.

**Answer 3:** I use the `is` operator quite often in my work.

Here is an online method which will use a [type pattern](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/is#type-pattern) to give you the `EType` as a variable:

<pre class="code">
  if(element is ElementType EType)
  {
    TaskDialog.Show("Type Name",EType.Name);
  }
</pre>

Interesting to note that in VB the `is` statement only returns true if the exact same type is compared.
Since `is` is used to check if two things are the same object instance.
The equivalent to the way it is used in C# seems to be:

<pre class="code">
  Dim B1 As Boolean = TypeOf Element Is ElementType
</pre>


#### <a name="2"></a>AI Ethics

A very interesting and promising first stab at creating 'good' AI and NLP that conforms with common human ethical judgement and behaviour is presented in the research paper 
on [Aligning AI With Shared Human Values](https://arxiv.org/pdf/2008.02275v1.pdf).

- preview_control_rotates_model.mp4
Used PreviewControl(ElementHost & PickPoint
https://forums.autodesk.com/t5/revit-api-forum/used-previewcontrol-elementhost-amp-pickpoint/m-p/9715680

- super cool checks for element type

**Question:** 

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 1166 -->
</center>

**Answer:**

####<a name="5"></a>

<!--

<pre class="code">
</pre>
-->

