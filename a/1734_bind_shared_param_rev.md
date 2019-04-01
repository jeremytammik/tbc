<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


twitter:

Binding a shared parameter to revision in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/bindsharedparam

Here is a recurring question on binding a shared parameter to a given category
&ndash; How to add a shared parameter to revision?
&ndash; Determine the category
&ndash; Binding to the category
&ndash; Implementation sample...

linkedin:

Binding a shared parameter to revision in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/bindsharedparam

Here is a recurring question on binding a shared parameter to a given category:

- How to add a shared parameter to revision?
- Determine the category
- Binding to the category
- Implementation sample...

of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.145.4).

-->

### Binding a Shared Parameter to Revision

Here is a recurring question that we have answered in depth a few times over, on binding a shared parameter to a given category, so the answer mainly consists of pointers to past discussions:

- [How to add a shared parameter to revision?](#2) 
- [Determine the category](#3) 
- [Binding to the category](#4) 
- [Implementation sample](#5) 

<center>
<img src="img/bind_and_package.jpg" alt="Bind and package" width="190">
</center>
 
#### <a name="2"></a> How to Add a Shared Parameter to Revision?

Is there an example to add a shared parameter to a Revision record?

How do I add more information to the Revision record in the parameters?


#### <a name="3"></a> Determine the Category

Afaict from experiments in the distant past, you can add a shared parameter to almost any category.

Does a category exist for revisions?

If you don't know off-hand, you can tell in several different ways:

- Explore the existing revision that you just created using RevitLookup; what category does it have?
- Look at the definition of the built-in category enumeration in Visual Studio and search for 'revision'.
- Look at the definition of the built-in category enumeration in the Revit API help documentation and search for 'revision'.

I did the latter, looked at
the [`BuiltInCategory` enumeration documentation](https://apidocs.co/apps/revit/2019/ba1c5b30-242f-5fdc-8ea9-ec3b61e6e722.htm).

That shows me that the built-in category `OST_Revisions` exists, so all is well so far.


#### <a name="4"></a> Binding to the Category

Next, I suggest you check out the [ADN Xtra labs](https://github.com/jeremytammik/AdnRevitApiLabsXtra).

Of special interest in your case is the external command Lab4_3_1_CreateAndBindSharedParam in
the [module Labs4.cs](https://github.com/jeremytammik/AdnRevitApiLabsXtra/blob/master/XtraCs/Labs4.cs) that
shows how to create and bind a shared parameter.

As you can see from
the [comments on that command](https://github.com/jeremytammik/AdnRevitApiLabsXtra/blob/master/XtraCs/Labs4.cs#L518-L539),
I have used it repeatedly in the past to test creating a shared parameter for various categories, both built-in ones such as `OST_Revisions` and dynamically generated ones, such as for an imported DWG file.

Search The Building Coder blog posts for Lab4_3_1_CreateAndBindSharedParam to see detailed discussions of some of those experiments:

- [Adding a Shared Parameter to a DWG File](http://thebuildingcoder.typepad.com/blog/2008/11/adding-a-shared-parameter-to-a-dwg-file.html)
- [Model Group Shared Parameter](http://thebuildingcoder.typepad.com/blog/2009/06/model-group-shared-parameter.html)
- [Exporting Parameter Data to Excel, and Re-importing](http://thebuildingcoder.typepad.com/blog/2012/09/exporting-parameter-data-to-excel.html)
- [Sydney Revit API Training](http://thebuildingcoder.typepad.com/blog/2013/07/sydney-revit-api-training-and-vacation.html)
- [Retrieving Element Properties](https://thebuildingcoder.typepad.com/blog/2015/06/archsample-active-transaction-and-adnrme-for-revit-mep-2016.html#2)
- [The FireRating Revit SDK Sample and ADN Xtra Labs](http://thebuildingcoder.typepad.com/blog/2015/07/firerating-and-the-revit-python-shell-in-the-cloud-as-web-servers.html)
- [Material Shared Parameters and ADN Xtra Labs](http://thebuildingcoder.typepad.com/blog/2016/12/material-shared-parameters-and-adn-xtra-labs.html)

#### <a name="5"></a> Implementation Sample

Once you have ascertained that you can bind a shared parameter to the category of interest, I assume your next question will be how to do so in a simple and efficient manner.

I implemented one approach for
the [ExportCncFab add-in](https://github.com/jeremytammik/ExportCncFab).

It is discussed in three posts by The Building Coder, listed in the topic group
on [splitting an element into parts](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.39).

The third of those explores and
implements [binding and storing shared parameter data](https://thebuildingcoder.typepad.com/blog/2013/12/driving-cnc-fabrication-and-shared-parameters.html#4).

