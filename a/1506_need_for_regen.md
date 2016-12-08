<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

Need for Regen and Duplicate Parameter Access #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

A Python Revit API question on renumbering viewports helps highlight two separate important programming aspects; I also add some other notes from the Munich Forge accelerator
&ndash; Question
&ndash; The need to regenerate
&ndash; Use built-in parameters to access parameters
&ndash; Forge accelerator and outage report
&ndash; Added materials to 210 King model...

-->

### Need for Regen and Duplicate Parameter Access

A Python Revit API question on renumbering viewports helps highlight two separate important programming aspects; I also add some other notes from the Munich Forge accelerator:

- [Question](#2)
- [The need to regenerate](#3)
- [Use built-in parameters to access parameters](#4)
- [Forge accelerator and outage report](#5)
- [Added materials to 210 King model](#6)


####<a name="2"></a>Question


The original issue was raised both in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread
on [help with 2 sequential transactions](http://forums.autodesk.com/t5/revit-api-forum/revit-api-help-with-2-sequential-transactions/m-p/6701715) and
a StackOverflow question 
on  [2 sequential transactions setting detail number (Revit API / Python)](http://stackoverflow.com/questions/40728351/2-sequential-transactions-setting-detail-number-revit-api-python/40735868).

The latter went on a bit longer than the former...

Rather a pain having the same discussion in multiple locations, but what can you do?

Anyway, I find the results interesting, so I'll do my best to consolidate them here and now.

> I implemented a tool to rename view numbers ('Detail Number') on a sheet based on their location on the sheet.
Where this is breaking is the transactions.
I'm trying to execute two transactions sequentially in Revit Python Shell.
I also did this originally in Dynamo, and that had a similar failure, so I know it's something to do with transactions.

- Transaction #1: Add a suffix ("-x") to each detail number to ensure the new numbers won’t conflict (1 will be 1-x, 4 will be 4-x, etc.)

- Transaction #2: Change detail numbers with calculated new number based on viewport location (1-x will be 3, 4-x will be 2, etc.)

Essentially, this is what I am trying to achieve:

<pre class="prettyprint">
  # <---- Make unique numbers    
  t = Transaction(doc, 'Rename Detail Numbers')
  t.Start()
  for i, viewport in enumerate(viewports):
    setParam(viewport, "Detail Number",getParam(viewport,"Detail Number")+"x")
  t.Commit()

  # <---- Do the thang        
  t2 = Transaction(doc, 'Rename Detail Numbers')
  t2.Start()
  for i, viewport in enumerate(viewports):
    setParam(viewport, "Detail Number",detailViewNumberData[i])
  t2.Commit()
</pre>

Here is [rename_view_numbers.pdf showing a visual explanation](zip/rename_view_numbers.pdf)
([^](https://www.docdroid.net/EP1K9Di/161115-viewport-diagram-.pdf.html)) and
the [complete Python file rename_view_numbers.py](zip/rename_view_numbers.py)
([^](http://pastebin.com/7PyWA0gV)).

<center>
<img src="img/set_parameter_read_only_error.png" alt="Set parameter throws read-only error" width="476"/>
</center>


####<a name="3"></a>The Need to Regenerate

Thank you for your query.

This is a nice example of the [need for regeneration](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.33).

The behaviour you describe may well be caused by a need to regenerate between the transactions. The first modification does something, and the model needs to be regenerated before the modifications take full effect and are reflected in the parameter values that you query in the second transaction. You are accessing stale data. Explore [The Building Coder topic group on the need to regenerate](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.33) to learn all the nitty gritty details and view numerous examples of this.

Also, when making use of several transactions in one go, it might also be helpful to check out the `TransactionGroup` class and its `Assimilate` method, e.g., in the discussion on [using transaction groups](http://thebuildingcoder.typepad.com/blog/2015/02/using-transaction-groups.html).
 

####<a name="4"></a>Use Built-in Parameters to Access Parameters

Accessing a parameter by its display name is extremely risky and inefficient to boot.

It is always better to use the built-in parameter enum values to access element parameters, when possible, or the parameter `Definition`, if you happen to have that at hand.

Here is the developer response:

> So, this issue actually had nothing to do with transactions or doc regeneration. I discovered (with some help :) ), that the problem lay in how I was setting/getting the parameter. The "Detail Number" on a viewport element, like a lot of Revit element parameters, has duplicate versions that share the same descriptive parameter display name.

<center>
<img src="img/duplicate_parameter_display_names.png" alt="Duplicate parameter display names" width="304"/>
</center>

Apparently, the reason for this might be a legacy issue, though I'm not sure.
When I was trying to get/set the detail number, it was somehow grabbing the incorrect read-only parameter occasionally, one associated with the built-in parameter enum value `VIEWER_DETAIL_NUMBER`.
The correct one to use is `VIEWPORT_DETAIL_NUMBER`.

This happened because I was trying to retrieve the parameter by passing the descriptive parameter display name "Detail Number".
Revising how I get/set parameters via the built-in parameter enum value resolved this issue,
cf. [rename_view_number_duplicates.pdf](zip/rename_view_number_duplicates.pdf)
([^](https://www.docdroid.net/WbAHBGj/161206-detail-number.pdf.html)).



####<a name="5"></a>Forge Accelerator and Outage Report

As you may know, I assisted at the Munich Forge Accelerator.

Unfortunately, we experienced
the [first major outage](http://adndevblog.typepad.com/cloud_and_mobile/2016/12/data-management-outage.html) of
the Forge Data Management and Model Derivative web services right in the middle of it.

Please refer to Kean Walmsley's report 
on [HoloLens and Forge at the Munich accelerator](http://through-the-interface.typepad.com/through_the_interface/2016/12/hololens-and-forge-at-the-munich-accelerator.html) for
more details on both of these topics.

All Forge services are now back online, including full API access to Fusion Team, A360 and BIM 360 Docs data.
The Forge team apologizes for the current outage and any problems it has caused.
We are now investigating all aspects of this outage to prevent it from happening in the future.
The first place to go to check is always the [Forge API status page](https://developer.autodesk.com/en/support/api-status).



####<a name="6"></a>Added Materials to 210 King Model

As part of my New Year celebration last January, I published
a [collection of public Revit models](http://thebuildingcoder.typepad.com/blog/2016/01/happy-new-top-cad-blog-public-models-and-forge.html#4)
on [Dropbox](https://www.dropbox.com/sh/51beylyeor41jnm/AAB7PaDA5t0H5Hg7DDgaslpla?dl=0).

One of them is the model of the [210 King Street Autodesk Toronto office](https://www.dropbox.com/sh/51beylyeor41jnm/AAATil-xM2Y8qC3N5WtmHQZca/210%20King%20-%20Autodesk%20Toronto.rvt?dl=0).

Philippe is using that in
his [responsive connected database viewer](https://forge-rcdb.autodesk.io) Forge
demonstration ([GitHub repo](https://github.com/Autodesk-Forge/forge-rcdb.nodejs)) and asked me to
help [clean up its material settings](http://thebuildingcoder.typepad.com/blog/2016/12/material-shared-parameters-and-adn-xtra-labs.html#7).

That led me to implement
the [PopulateMaterialProperty add-in](https://github.com/jeremytammik/PopulateMaterialProperty).

In case you are interested in the result of running this on the Toronto office model in Revit 2017, The Building Coder has now made that available
as [210_King_2017_6.rvt](http://thebuildingcoder.typepad.com/public_revit_model/210_King_2017_6.rvt).

