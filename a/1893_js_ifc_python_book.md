<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Use relative paths (%appdata%) in .addin file
  https://forums.autodesk.com/t5/revit-api-forum/use-relative-paths-appdata-in-addin-file/m-p/10074984

- best book for Python for Revit and Dynamo
  https://forums.autodesk.com/t5/revit-api-forum/books-or-other-sources-to-learn-python-to-be-used-in-revit/m-p/10063424/highlight/false#M53185

- RevitPythonShell requires a new maintainer
  daren-thomas commented 20 days ago
  > As of next month, I will not have access to Revit anymore, and the project will need a new maintainer.
  https://github.com/architecture-building-systems/revitpythonshell/issues/111

- IFC.js
  https://github.com/agviegas/IFC.js
  https://www.aechive.net/agviegas/ifc-js-em4

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; ...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
<p style="font-size: 80%; font-style:italic">
<a href=""></a>
</p>
</center>

-->

### IFC.js, Learning Python Books and Hints

####<a name="2"></a> Persnalised Add-In Manifest


Andrea Tassera


Andrea Tassera of [Woods Bagot](https://www.woodsbagot.com) raised an interesting question in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [using relative paths (`%appdata%`) in `.addin` file](https://forums.autodesk.com/t5/revit-api-forum/use-relative-paths-appdata-in-addin-file/m-p/10074984):

**Question:** 


Is it possible to use a relative path, such as %appdata% in the Assembly section of the addin file?

I am developing this plugin that will be deployed to other people in the company, and the DLLs will live somewhere in C:\Users\<USERNAME>\AppData\Roaming\NameOfFolder\... so writing the explicit path with the user name is not a real option. Normally in Windows you would use %appdata% but it doesn't seem to be working in the .addin.

Is there a way?

This is what I tried but isn't working:

<pre>
<?xml version="1.0" encoding="utf-8"?>
<RevitAddIns>
 <AddIn Type="Application">
       <Name>Wb.ModelEstablishment.Revit</Name>
       <FullClassName>Wb.ModelEstablishment.Revit.Ribbon</FullClassName>
       <Text>Wb.ModelEstablishment.Revit</Text>
       <Description>Model Establishment</Description>
       <VisibilityMode>AlwaysVisible</VisibilityMode>
       <Assembly>%appdata%\folder\subfolder\Wb.ModelEstablishment.Revit.dll</Assembly>
       <AddInId>d06838e1-44e3-4c05-b9f1-f79ca101075c</AddInId>
    <VendorId>WB</VendorId>
    <VendorDescription>Woods Bagot</VendorDescription>
 </AddIn>
</RevitAddIns>
</pre>

But if I use:

<pre>
<Assembly>C:\Users\sydata\AppData\Roaming\folder\subfolder\Wb.ModelEstablishment.Revit.dll</Assembly>
it works fine.
</pre>

**Answer:** Yes, it is definitely possible to use relative paths in the add-in manifest.

However, `%appdata%` is not a relative path.

That is a variable in an MS-DOS or Windows batch file, or possibly nowadays in a PowerShell script or something suchlike.

Revit add-in manifest files do not support variables, neither MS-DOS nor Windows nor Unix nor any other flavour.

You can read about what is and is not supported in manifest files in the Revit online help section
on [add-in registration](https://help.autodesk.com/view/RVT/2021/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_Add_in_Registration_html).

Look there for 'In a non-user-specific location in "application data"'.

**Response:** I have read the page you sent, but the whole 'user-specific' or 'non-user specific "application data"' talks about where to save the `.addin` file, not how to point to dlls placed in the `%appdata%` folder through the `Assembly` XML tag inside the `.addin` file.

The .addin file can be in the user folder:

- C:\Users\<user>\AppData\Roaming\Autodesk\Revit\Addins\...

That's not a problem.
The problem is that the `Assembly` tag needs to point to a roaming folder:

- C:\Users\<user>\AppData\Roaming\...

I don't know how to achieve that without using the absolute path.

I can't write one specific addin file for every user in the office.

Does it make sense?

**Answer:** Yes, absolutely, it makes perfect sense.

As said, the `%appdata%` variable that you are referring to is a Windows specific variable that is not understood or supported by the add-in manifest file.

One simple option I see for you is to (automatically) generate a user-specific add-in manifest for each user and place each one in the appropriate user-specific location.

Another possible approach would be to place one single add-in manifest file for all users into the system-wide global location, and then use other run-time criteria to determine whether or not to make individual add-in functionality and components available to each user on a case-by-case basis, e.g., using the Revit API `AvailabilityClassName`.

Oh, and another thing, much simpler:

I almost never use the add-in assembly DLL path at all.

I just place the add-in assembly DLL in the same location as the add-in manifest `*.addin` file, and then it is found without specifying any path at all.

If you are already placing the add-in manifest file in the user-specific location, why don't you just put the DLL in the same place?

**Response:** Thanks for clarifying. I get what you mean now!

Unfortunately, the DLLs are in the `%appdata%` folder because they come through `pyRevit`, and the repository gets pulled under:

- %appdata%\pyRevit\Extensions\...

Usually, pyRevit doesn't need addin files, but this is a linkbutton, because I need things to happen at application level (like registering a dockable panel), so it needs to have an addin file to load the DLLs when the Revit application is loading.
It's a bit more complex than usual.

We could maybe write a PowerShell script that moves those DLLs somewhere else, but it seems a bit convoluted.

Creating an automatic generator of the addin sounds interesting though.
From the docs you posted, I don't understand where that code would live though.
It says that "It is intended for use from product installers and scripts".
Does that mean something like a PowerShell script?

**Answer:** You mean the RevitAddInUtility.dll?

The answer is Yes, cf. The Building Coder article
on [RevitAddInUtility](https://thebuildingcoder.typepad.com/blog/2010/04/revitaddinutility.html).


<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 2680 -->
</center>


####<a name="3"></a> 

<pre class="code">
</pre>

####<a name="4"></a> 

