<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- <script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script> -->
<!-- https://highlightjs.org/#usage -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
</head>

<!---

- Revit ID Compilation
  Revit IDs
  https://forums.autodesk.com/t5/revit-api-forum/revit-ids/td-p/12418195
  ####<a name="2"></a> Revit ID Compilation
  Revit ID Compilation
  Revit IDs
  https://forums.autodesk.com/t5/revit-api-forum/revit-ids/td-p/12418195
  >>>>Element Id
  0344:Newly Created Element Retrieval Based on Monotonously Increasing Element Id Values
  0344:Enhanced Parameter Filter for Greater Element Id Values
  0544:Comparing Element Id for Equality
  0948:Element Ids in Extensible Storage
  1144:Element Id &ndash; Export, Unique, Navisworks and Other Ids
  1144:Negative Element Ids and Element Property Drop-down List Enumerations <!-- dropdown combo -->
  1182:How to Trigger a Dynamic Model Updater by Specific Element Ids
  1353:Family Category, Element Ids, Transaction and Updates
  1396:<"#6">WPF Element Id Converter
  1577:<"#3">Access Revit BIM Data and Element Ids from BIM360
  1628:Retrieving Newly Created Element Ids
  1628:<"#3">Consecutive Element Ids
  1634:<"#2">Search and Snoop by Element Id or Unique Id
  1762:Element Identifiers in RVT, IFC, NW and Forge
  1762:<"#3"> Revit Element Ids in Forge via Navisworks and IFC
  1959:<"#2"> Immutable UniqueId, Mutable Element Id
  1974:64-Bit Element Ids, Maybe?
  1974:<"#2"> 64-Bit Element Ids
  1992:<"#9"> Consuming Huge Numbers of Element Ids
  1995:<"#4"> Backward Compatible 64 Bit Element Id
  >>>>0344 0544 0948 1144 1182 1353 1396 1577 1628 1634 1762 1959 1974 1992 1995
  <ul>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2010/04/retrieving-newly-created-elements-in-revit-2011.html">Retrieving Newly Created Elements in Revit 2011</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2011/02/comparing-element-id-for-equality.html">Comparing Element Id for Equality</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2013/05/dwg-issues-and-various-other-updates.html">DWG Issues and Various Other Updates</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2014/04/element-id-export-unique-navisworks-and-other-ids.html">Element Id &ndash; Export, Unique, Navisworks and Other Ids</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2014/07/createlinkreference-sample-code.html">CreateLinkReference Sample Code</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2015/09/family-category-element-ids-transaction-undo-and-updates.html">Family Category, Element Ids, Transaction Undo and Updates</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2016/01/devday-conference-in-munich-and-wpf-doevents.html">DevDay Conference in Munich and WPF DoEvents</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2017/08/revit-versus-forge-ids-and-add-in-installation.html">Revit versus Forge, Ids and Add-In Installation</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2018/02/retrieving-newly-created-element-ids.html">Retrieving Newly Created Element Ids</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2018/03/revitlookup-search-and-snoop-by-element-and-unique-id.html">RevitLookup Search by Element and Unique Id</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2019/07/element-identifiers-in-rvt-ifc-nw-and-forge.html">Element Identifiers in RVT, IFC, NW and Forge</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2022/07/immutable-uniqueid-and-revit-database-explorer.html">Immutable UniqueId and Revit Database Explorer</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2022/11/64-bit-element-ids-maybe.html">64-Bit Element Ids, Maybe?</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2023/04/configuring-rvtsamples-2024.html">Configuring RvtSamples 2024 and Big Numbers</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2023/05/64-bit-ids-revit-and-revitlookup-updates.html">64 Bit Ids, Revit and RevitLookup Updates</a></li>
  </ul>
  >>>>Unique Id
  0104:UniqueId versus DWF and IFC GUID
  0104:GUID and UniqueId
  0104:UniqueId to GUID Encoding
  0104:IFC GUID and UniqueId Encoder and Decoder
  0737:Retrieving Unique Geometry Vertices
  0787:Geometry Traversal to Retrieve Unique Vertices
  0819:IFC GUID Generation and Uniqueness
  0943:Solving the Non-unique Unique Id Problem
  1144:Element Id &ndash; Export, Unique, Navisworks and Other Ids
  1144:Unique Id versus ElementId to Store in External Database
  1144:Local Uniqueness of the Revit Unique Id
  1144:Navisworks versus Revit Object Unique Ids
  1144:Revit Id and UniqueId Lost On Reimporting Revised Model
  1209:Unique Names and the NamingUtils Class
  1277:Understanding the Use of the UniqueId
  1304:Extracting Unique Building Element Geometry Vertices
  1459:Consistency of IFC GUID and UniqueId
  1577:<"#4">Unique IDs for Forge Viewer Elements
  1634:RevitLookup Search by Element and Unique Id
  1634:<"#2">Search and Snoop by Element Id or Unique Id
  1949:Unique Id and IFC GUID Parameter
  1949:<"#4"> You Cannot Control the Unique Id
  1959:Immutable UniqueId and Revit Database Explorer
  1959:<"#2"> Immutable UniqueId, Mutable Element Id
  >>>> 0104 0737 0787 0819 0943 1144 1209 1277 1304 1459 1577 1634 1949 1959
  <ul>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2009/02/uniqueid-dwf-and-ifc-guid.html">UniqueId, DWF and IFC GUID</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2012/03/melbourne-day-two.html">Melbourne Day Two</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2012/06/real-world-concrete-corner-coordinates.html">Real-World Concrete Corner Coordinates</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2012/09/ifc-guid-generation-and-uniqueness.html">IFC GUID Generation and Uniqueness</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2013/05/copy-and-paste-api-applications-and-modeless-assertion.html">Copy and Paste API Applications and Modeless Assertion</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2014/04/element-id-export-unique-navisworks-and-other-ids.html">Element Id &ndash; Export, Unique, Navisworks and Other Ids</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2014/09/unique-names-and-the-namingutils-class.html">Unique Names and the NamingUtils Class</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2015/02/understanding-the-use-of-the-uniqueid.html">Understanding the Use of the UniqueId</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2015/04/back-from-easter-holidays-and-various-revit-api-issues.html">Back from Easter Holidays and Various Revit API Issues</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2016/08/consistency-of-ifc-guid-and-uniqueid.html">Consistency of IFC GUID and UniqueId</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2017/08/revit-versus-forge-ids-and-add-in-installation.html">Revit versus Forge, Ids and Add-In Installation</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2018/03/revitlookup-search-and-snoop-by-element-and-unique-id.html">RevitLookup Search by Element and Unique Id</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2022/04/unique-id-and-ifc-guid.html">Unique Id and IFC GUID Parameter</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2022/07/immutable-uniqueid-and-revit-database-explorer.html">Immutable UniqueId and Revit Database Explorer</a></li>
  </ul>

- https://forums.autodesk.com/t5/revit-api-forum/are-references-unique-across-documents/td-p/12381420

- thoughts on revit precision
  SpatialElementGeometryCalculator not accurate
  https://forums.autodesk.com/t5/revit-api-forum/spatialelementgeometrycalculator-not-accurate/m-p/12417416

- need for fuzz:
  What is Fuzz?
  https://thebuildingcoder.typepad.com/blog/2023/03/uv-emergence-fuzz-and-the-get_-prefix.html#4
  Again, the Need for Fuzz
  https://thebuildingcoder.typepad.com/blog/2022/08/instances-in-room-and-need-for-fuzz.html#3
  It is very important for every programmer dealing with geometry and CAD to understand that it is impossible to exactly represent a floating point number in a digital computer. Hence, the need for fuzz when comparing two numbers:
  https://www.google.com/search?q=fuzz&as_sitesearch=thebuildingcoder.typepad.com
  To avoid any deviation in a series P1, P2, ... Pn of vertical points, you can proceed as follows. Pick one of the collinear vertical points. It does not matter which one it is, and any one will do, e.g., the bottom one. Let's assume that is P1 with coordinates (x1,y1,z1). Now, replace the entire series of points P1,...Pn by a modified series P1,Q2,Q3,...Qn by defining each Q as follows:
  Qi = (x1,y1,zi) for i = 2, 3, ... n
  In other words, define all the Q so that they lie exactly vertically above P1.
  Since the Pi are all supposed to be vertical, the difference between their x and y coordinates must be negligeable.
  So, ignore it, and Bob's your uncle. Good luck.

- DirectContext3D: API for Displaying External Graphics in Revit
  by Alex Pytel
  https://www.autodesk.com/autodesk-university/class/DirectContext3D-API-Displaying-External-Graphics-Revit-2017#video

- a novel method using tagging to
  Determine linked elements present in a section
  https://forums.autodesk.com/t5/revit-api-forum/determine-linked-elements-present-in-a-section/td-p/12488150

twitter:

 #BIM  @AutodeskRevit #RevitAPI @AutodeskAPS @DynamoBIM

&ndash;  ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Revit Ids and Precision versus Fuzz


####<a name="2"></a>

**Question:**

**Answer:**

<center>
<img src="img/" alt="" title="" width="100"/> <!-- Pixel Height: 627 Pixel Width: 861 -->
</center>



####<a name="2"></a> Revit ID Compilation

Revit ID Compilation

  Revit IDs
  https://forums.autodesk.com/t5/revit-api-forum/revit-ids/td-p/12418195
  ####<a name="2"></a> Revit ID Compilation
  Revit ID Compilation
  Revit IDs
  https://forums.autodesk.com/t5/revit-api-forum/revit-ids/td-p/12418195
  >>>>Element Id
  0344:Newly Created Element Retrieval Based on Monotonously Increasing Element Id Values
  0344:Enhanced Parameter Filter for Greater Element Id Values
  0544:Comparing Element Id for Equality
  0948:Element Ids in Extensible Storage
  1144:Element Id &ndash; Export, Unique, Navisworks and Other Ids
  1144:Negative Element Ids and Element Property Drop-down List Enumerations <!-- dropdown combo -->
  1182:How to Trigger a Dynamic Model Updater by Specific Element Ids
  1353:Family Category, Element Ids, Transaction and Updates
  1396:<"#6">WPF Element Id Converter
  1577:<"#3">Access Revit BIM Data and Element Ids from BIM360
  1628:Retrieving Newly Created Element Ids
  1628:<"#3">Consecutive Element Ids
  1634:<"#2">Search and Snoop by Element Id or Unique Id
  1762:Element Identifiers in RVT, IFC, NW and Forge
  1762:<"#3"> Revit Element Ids in Forge via Navisworks and IFC
  1959:<"#2"> Immutable UniqueId, Mutable Element Id
  1974:64-Bit Element Ids, Maybe?
  1974:<"#2"> 64-Bit Element Ids
  1992:<"#9"> Consuming Huge Numbers of Element Ids
  1995:<"#4"> Backward Compatible 64 Bit Element Id
  >>>>0344 0544 0948 1144 1182 1353 1396 1577 1628 1634 1762 1959 1974 1992 1995
  <ul>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2010/04/retrieving-newly-created-elements-in-revit-2011.html">Retrieving Newly Created Elements in Revit 2011</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2011/02/comparing-element-id-for-equality.html">Comparing Element Id for Equality</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2013/05/dwg-issues-and-various-other-updates.html">DWG Issues and Various Other Updates</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2014/04/element-id-export-unique-navisworks-and-other-ids.html">Element Id &ndash; Export, Unique, Navisworks and Other Ids</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2014/07/createlinkreference-sample-code.html">CreateLinkReference Sample Code</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2015/09/family-category-element-ids-transaction-undo-and-updates.html">Family Category, Element Ids, Transaction Undo and Updates</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2016/01/devday-conference-in-munich-and-wpf-doevents.html">DevDay Conference in Munich and WPF DoEvents</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2017/08/revit-versus-forge-ids-and-add-in-installation.html">Revit versus Forge, Ids and Add-In Installation</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2018/02/retrieving-newly-created-element-ids.html">Retrieving Newly Created Element Ids</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2018/03/revitlookup-search-and-snoop-by-element-and-unique-id.html">RevitLookup Search by Element and Unique Id</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2019/07/element-identifiers-in-rvt-ifc-nw-and-forge.html">Element Identifiers in RVT, IFC, NW and Forge</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2022/07/immutable-uniqueid-and-revit-database-explorer.html">Immutable UniqueId and Revit Database Explorer</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2022/11/64-bit-element-ids-maybe.html">64-Bit Element Ids, Maybe?</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2023/04/configuring-rvtsamples-2024.html">Configuring RvtSamples 2024 and Big Numbers</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2023/05/64-bit-ids-revit-and-revitlookup-updates.html">64 Bit Ids, Revit and RevitLookup Updates</a></li>
  </ul>
  >>>>Unique Id
  0104:UniqueId versus DWF and IFC GUID
  0104:GUID and UniqueId
  0104:UniqueId to GUID Encoding
  0104:IFC GUID and UniqueId Encoder and Decoder
  0737:Retrieving Unique Geometry Vertices
  0787:Geometry Traversal to Retrieve Unique Vertices
  0819:IFC GUID Generation and Uniqueness
  0943:Solving the Non-unique Unique Id Problem
  1144:Element Id &ndash; Export, Unique, Navisworks and Other Ids
  1144:Unique Id versus ElementId to Store in External Database
  1144:Local Uniqueness of the Revit Unique Id
  1144:Navisworks versus Revit Object Unique Ids
  1144:Revit Id and UniqueId Lost On Reimporting Revised Model
  1209:Unique Names and the NamingUtils Class
  1277:Understanding the Use of the UniqueId
  1304:Extracting Unique Building Element Geometry Vertices
  1459:Consistency of IFC GUID and UniqueId
  1577:<"#4">Unique IDs for Forge Viewer Elements
  1634:RevitLookup Search by Element and Unique Id
  1634:<"#2">Search and Snoop by Element Id or Unique Id
  1949:Unique Id and IFC GUID Parameter
  1949:<"#4"> You Cannot Control the Unique Id
  1959:Immutable UniqueId and Revit Database Explorer
  1959:<"#2"> Immutable UniqueId, Mutable Element Id
  >>>> 0104 0737 0787 0819 0943 1144 1209 1277 1304 1459 1577 1634 1949 1959
  <ul>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2009/02/uniqueid-dwf-and-ifc-guid.html">UniqueId, DWF and IFC GUID</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2012/03/melbourne-day-two.html">Melbourne Day Two</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2012/06/real-world-concrete-corner-coordinates.html">Real-World Concrete Corner Coordinates</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2012/09/ifc-guid-generation-and-uniqueness.html">IFC GUID Generation and Uniqueness</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2013/05/copy-and-paste-api-applications-and-modeless-assertion.html">Copy and Paste API Applications and Modeless Assertion</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2014/04/element-id-export-unique-navisworks-and-other-ids.html">Element Id &ndash; Export, Unique, Navisworks and Other Ids</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2014/09/unique-names-and-the-namingutils-class.html">Unique Names and the NamingUtils Class</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2015/02/understanding-the-use-of-the-uniqueid.html">Understanding the Use of the UniqueId</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2015/04/back-from-easter-holidays-and-various-revit-api-issues.html">Back from Easter Holidays and Various Revit API Issues</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2016/08/consistency-of-ifc-guid-and-uniqueid.html">Consistency of IFC GUID and UniqueId</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2017/08/revit-versus-forge-ids-and-add-in-installation.html">Revit versus Forge, Ids and Add-In Installation</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/blog/2018/03/revitlookup-search-and-snoop-by-element-and-unique-id.html">RevitLookup Search by Element and Unique Id</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2022/04/unique-id-and-ifc-guid.html">Unique Id and IFC GUID Parameter</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2022/07/immutable-uniqueid-and-revit-database-explorer.html">Immutable UniqueId and Revit Database Explorer</a></li>
  </ul>

####<a name="2"></a> https://forums.autodesk.com/t5/revit-api-forum/are-references-unique-across-documents/td-p/12381420

https://forums.autodesk.com/t5/revit-api-forum/are-references-unique-across-documents/td-p/12381420


####<a name="2"></a> thoughts on revit precision

thoughts on revit precision

  SpatialElementGeometryCalculator not accurate
  https://forums.autodesk.com/t5/revit-api-forum/spatialelementgeometrycalculator-not-accurate/m-p/12417416

####<a name="2"></a> need for fuzz:

need for fuzz:

  What is Fuzz?
  https://thebuildingcoder.typepad.com/blog/2023/03/uv-emergence-fuzz-and-the-get_-prefix.html#4
  Again, the Need for Fuzz
  https://thebuildingcoder.typepad.com/blog/2022/08/instances-in-room-and-need-for-fuzz.html#3
  It is very important for every programmer dealing with geometry and CAD to understand that it is impossible to exactly represent a floating point number in a digital computer. Hence, the need for fuzz when comparing two numbers:
  https://www.google.com/search?q=fuzz&as_sitesearch=thebuildingcoder.typepad.com
  To avoid any deviation in a series P1, P2, ... Pn of vertical points, you can proceed as follows. Pick one of the collinear vertical points. It does not matter which one it is, and any one will do, e.g., the bottom one. Let's assume that is P1 with coordinates (x1,y1,z1). Now, replace the entire series of points P1,...Pn by a modified series P1,Q2,Q3,...Qn by defining each Q as follows:
  Qi = (x1,y1,zi) for i = 2, 3, ... n
  In other words, define all the Q so that they lie exactly vertically above P1.
  Since the Pi are all supposed to be vertical, the difference between their x and y coordinates must be negligeable.
  So, ignore it, and Bob's your uncle. Good luck.

####<a name="2"></a> AU Class on DirectContext3D

Here is a go-to source of information for `DirectContext3D` that you should be aware of when dealing with this topic,
an Autodesk University 2023 class by Alex Pytel:

- [DirectContext3D: API for Displaying External Graphics in Revit](https://www.autodesk.com/autodesk-university/class/DirectContext3D-API-Displaying-External-Graphics-Revit-2017#video)

####<a name="2"></a> Determining Elements Present in Section View

Faced with the task of determining which elements are present in a given section view, we stumbled across a novel solution using tagging in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread on
how to [determine linked elements present in a section](https://forums.autodesk.com/t5/revit-api-forum/determine-linked-elements-present-in-a-section/td-p/12488150):

**Question:** I have a section with some project elements, and a linked file loaded with some elements in the view, as shown here:

<center>
<img src="img/linked_elements_in_section.png" alt="Linked elements in section view" title="Linked elements in section view" width="500"/> <!-- Pixel Height: 718 Pixel Width: 881 -->
</center>

I need to check which elements of the linked file are present in this section, but I'm having difficulties.
I tried to check whether the elements are inside the view's BoundingBox, but without success.
Then, I tried to apply the solution
to [Check if a point is inside bounding box](https://forums.autodesk.com/t5/revit-api-forum/check-to-see-if-a-point-is-inside-bounding-box/td-p/4354446).

However, it looks like the position returned in BoundingBox is outside of view.

I use this solution to apply tags to linked elements without any problems:

<pre><code>
Reference refe = new Reference(itemconex)
  .CreateLinkReference(docsVinculados);

IndependentTag tagConexao = IndependentTag.Create(
  Doc.Document, TagConexSelecionada.Id, Doc.ActiveView.Id,
  refe, true, TagOrientation.Horizontal, PosicaoFinal);
</code></pre>

**Answer:** The biggest challenge is probably the transformation.
One possible approach would be to read and understand in depth all the transformations involved.
Another possible approach, in case your tendency is stronger to hack and do rather than study and ponder, might be: create a very simple linked file with just an element or two, e.g., model lines.
Host it.
Analyse the resulting geometry in the host file.
Reproduce the model lines in the host file until their appearance matches the original ones in the linked file.
Basically, you just need to determine where a given bounding box in the linked file will end up on the host, don't you?

Since you mention that you can successfully and automatically create tags for the linked elements, another idea comes to mind: before creating the tags, subscribe to the `DocumentChanged` method to be notified of the added elements.
Then, you can query the tags for their locations.
That will presumably approximate the host project locations of the linked elements.

**Response:** Hi Jeremy, thank you for your reply, you just opened my mind to a possible solution!
I just add a new `IndependentTag` then check if it's `BoundingBox` is valid:

<pre><code>
IndependentTag tagConexao = IndependentTag.Create(
  Config.doc, Config.doc.ActiveView.Id, refe, true,
  TagMode.TM_ADDBY_CATEGORY, TagOrientation.Horizontal,
  PosicaoFinal);

if (null != tagConexao.get_BoundingBox(Config.doc.ActiveView))
{
  // The element is in the view
}
</code></pre>

Then, I collect the id's I need and RollBack the transaction in the final, it's working.
Btw I'll study how transformations works when I have linked elements in some view.
Thank you.



