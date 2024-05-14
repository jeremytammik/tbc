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

- highlight elements from a linked document
  https://forums.autodesk.com/t5/revit-api-forum/highlight-elements-from-a-linked-document/td-p/12768033

- Modify Duct Length in Revit API Despite Read-Only Property Constraint
  https://forums.autodesk.com/t5/revit-api-forum/modify-duct-length-in-revit-api-despite-read-only-property/m-p/12763233

- How to detect is opened preview document in type properties?
  https://forums.autodesk.com/t5/revit-api-forum/how-to-detect-is-opened-preview-document-in-type-properties/m-p/12768772

twitter:

Highlighting linked elements using SetReferences, modifying duct length and determining whether a form is displayed using an IsMainWindowActive predicate in the @AutodeskRevit #RevitAPI #BIM @DynamoBIM https://autode.sk/hilitelinkelem

Highlighting linked elements using SetReferences, modifying duct length and determining whether a form is displayed using an IsMainWindowActive predicate...

linkedin:

Highlighting linked elements using SetReferences, modifying duct length and determining whether a form is displayed using an IsMainWindowActive predicate in the #RevitAPI

https://autode.sk/hilitelinkelem

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Lengthen Ducts and Highlight Links

Today, let's stick with some pure Revit API issues fresh from the forum:

- [Highlight linked element](#2)
- [Modify duct length](#3)
- [IsMainWindowActive predicate](#4)

####<a name="2"></a> Highlight Linked Element

Lately, Moustafa Khalil very kindly provided a lot of helpful support in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160).

He now took a step further, sharing his research and explanation on how
to [highlight elements from a linked document](https://forums.autodesk.com/t5/revit-api-forum/highlight-elements-from-a-linked-document/td-p/12768033),
a frequently raised topic, and even starting a new BIM blog, saying:

The past 2 days I was scratching my head of how to highlight an element from a linked document.
I tried different API approaches and found several existing posts requesting this, e.g.:

- [How to select linked element by element Id](https://forums.autodesk.com/t5/revit-api-forum/how-to-select-linked-element-by-element-id/m-p/8245634)
- [highlight and tag linked elements](https://forums.autodesk.com/t5/revit-api-forum/highlight-and-tag-linked-elements/m-p/5294217)
- [How to highlight element in Linked model by API](https://forums.autodesk.com/t5/revit-api-forum/how-to-highlight-element-in-linked-model-by-api/m-p/9945959)
- [High light Link Element](https://forums.autodesk.com/t5/revit-api-forum/high-light-link-element/m-p/11111305)
- [Can't select elements in a linked model](https://forums.autodesk.com/t5/revit-api-forum/can-t-select-elements-in-a-linked-model/m-p/12681983)

There is a corresponding wish in the Revit Idea Station:

- [Highlight element selection in linked files](https://forums.autodesk.com/t5/revit-ideas/highlight-element-selection-in-linked-files/idi-p/7619701)

The good news, after reading over the Revit API docs: it seems this wish has been granted since Revit 2023.

A new `Selection` function called `SetReferences` was added, allowing elements to be highlighted via a set of references.
I don't often use references to highlight elements, but rather to set hosts, like placing hosted families or extracting element IDs from a `ReferenceIntersector` or when selecting by picking.

So, if we provide the `SetReferences` function with references from a linked document, will it work?
Yes.
However, some extra work is required to capture such element references.
Firstly, we need to understand that this function operates on the currently active document.
This means that the references we provide must be in a format that the current document can recognize to highlight them in the current view.

Let's attempt to highlight a face from an element in a linked document in the following steps:

- Click on a point over one of the faces in a linked document.
- Pass this reference to `SetReferences`, and it will highlight the face from the linked document.
- Similarly, if you press Tab to cycle through line, face, and object, once you reach the object, select it to get the object reference.

<pre><code class="language-cs">var linkedFaceReference = UiDoc.Selection.PickObject(
  Autodesk.Revit.UI.Selection.ObjectType.PointOnElement
);
UiDoc.Selection.SetReferences([linkedFaceReference]);
</code></pre>

Now, this works when a user interacts with the UI.
What if I have an element ID from a linked document that I want to highlight?
The real question then becomes, how can I extract a reference from an `ElementId` that belongs to a linked document?

This is achievable, but not directly from the `ElementId`;
we need to work with the element itself.
First, we need to get the element from the linked document, then create a reference for this element.
However, this reference is only meaningful to the linked document, not the current one.
We can convert it to the current document using `CreateLinkReference` and the `RevitLinkInstance`.
The code below clearly demonstrates how it functions.
If you already have the linked `ElementId`, you can directly start from line 10, without the need for selection:

<pre><code class="language-cs"> var pickedReference = UiDoc.Selection.PickObject(
  Autodesk.Revit.UI.Selection.ObjectType.PointOnElement
);

// get Revit link Instance and its document
var linkedRvtInstance = Doc.GetElement(pickedReference) as RevitLinkInstance;
var linkedDoc = linkedRvtInstance.GetLinkDocument();

//get the Linked element from the linked document
var linkedElement = linkedDoc.GetElement(pickedReference.LinkedElementId);

// now create a reference from this element
// -- this is a reference inside the linked document
var reference = new Reference(linkedElement);

// convert the reference to be readable from the current document
reference = reference.CreateLinkReference(linkedRvtInstance);

// now the linked element is highlighted
UiDoc.Selection.SetReferences([reference]);
</code></pre>

<center>
<img src="img/highlight_linked_element.gif" alt="Highlight linked element" title="Highlight linked element" width="599"/> <!-- Pixel Height: 358 Pixel Width: 599 -->
</center>

Yes, the proposed solution is tested and works for me... I will be glad to know if there are any exception to this methodology.

I have also published this on my fresh starting [Sharp BIM blog](https://sharpbim.hashnode.dev);
I will usually journal my findings there as well as here in the forum:

- [Highlight elements from a linked document](https://sharpbim.hashnode.dev/highlight-elements-from-a-linked-document)

Many thanks to Moustafa for this clear explanation and demonstration, and for all his other great support in the discussion forum!

Best of luck and much success with your new blog!

####<a name="3"></a> Modify Duct Length

Moustafa also helped resolve how
to [modify duct length in Revit API despite read-only property constraint](https://forums.autodesk.com/t5/revit-api-forum/modify-duct-length-in-revit-api-despite-read-only-property/m-p/12763233):

**Question:**
I'm wondering if it's possible to alter the length of a duct in Revit through the API.
Upon trying, I noticed that the duct length property appears to be set as read-only.
Is there a workaround to modify the duct length?

**Answer:**
Yes, it can be done.
The API wraps the UI functionality, so the best way to address this is to determine the optimal workflow and best practices manually in the user interface first.
How do you solve this in the UI?

So, the API does not directly support changing the duct length.
One workaround is to delete the existing one and create a new duct with a new length, then update the neighbouring duct length according to that:

<pre><code class="language-cs">  UIDocument uiDoc = commandData.Application.ActiveUIDocument;
  Document doc = uiDoc.Document;

  Reference refer = uiDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);

  Duct duct = doc.GetElement(refer) as Duct;

  ///New Length Dimension
  double newLength = UnitUtils.ConvertToInternalUnits(10000,UnitTypeId.Millimeters);

  ///Calculating New Length
  LocationCurve curve = duct.Location as LocationCurve;
  XYZ p1 = curve.Curve.GetEndPoint(0);
  XYZ p2 = p1 + ((curve.Curve as Line).Direction * newLength);

  using (Transaction deleteDuctAndCreateNew = new Transaction(doc, "Delete Existing Duct and Create New"))
  {
    deleteDuctAndCreateNew.Start();

    //Create New Duct
    Duct.Create(doc, duct.MEPSystem.GetTypeId(),duct.GetTypeId(), duct.ReferenceLevel.Id, p1, p2);

    doc.Delete(duct.Id);

    deleteDuctAndCreateNew.Commit();
  }
</code></pre>

<center>
<img src="img/change_duct_length.gif" alt="Change duct legth" title="Change duct legth" width="600"/> <!-- Pixel Height: 559 Pixel Width: 999 -->
</center>

However, deleting an existing element means disconnecting it from the System and losing all instance property values such as mark or comment.

I would be more inclined to  only increase the length of the MepCurve (duct, pipe, conduit...etc.):

<pre><code class="language-cs">var locCurve = ductObject.Location as LocationCurve;
locCurve.Curve = extendedCurve;
</code></pre>

If the duct is connected to neighbouring elements, you can let Revit modify and adapt its length automatically by moving those neighbours and their connection points.
Look at an exploration of different approaches to modifying pipe length in the blog post series
on [implementing a rolling offset](http://thebuildingcoder.typepad.com/blog/2014/01/final-rolling-offset-using-pipecreate.html).

Just moving the neighbour elements will keep all the connections intact.

To add another approach, for those MEP curves without neighbour connections:
We may also extend the curve directly by its connector, which means no new line or assigning a location curve is needed:

<pre><code class="language-cs">Connector connector = getMyConnector();
double extendby = 1; // extend by 1 feet for example
XYZ direction = ductCurve.Direction; // assuming the duct is linear curve
connector.Origin = connector.Origin + direction * extendby;
</code></pre>

Thank you both, Mohamed Arshad K and Moustafa Khalil, for chipping in on this!

####<a name="4"></a> IsMainWindowActive Predicate

Aleksandr '@ModPlus' Pekshev raised the question and shared his working solution
for [how to detect is opened preview document in type properties](https://forums.autodesk.com/t5/revit-api-forum/how-to-detect-is-opened-preview-document-in-type-properties/m-p/12768772):

**Question:**
There is a `Preview` button in the type properties dialog.
If you click it, then, as far as I know, a copy of the current document will be created with a new view (I could be wrong here):

<center>
<img src="img/type_properties_preview.png" alt="Type properties preview" title="Type properties preview" width="600"/> <!-- Pixel Height: 559 Pixel Width: 999 -->
</center>

The problem is that in this case `IUpdater` is triggered, which can lead to negative consequences.

Question: how can I detect that this Preview is open, or how can I detect that the dialog for editing type properties is open?

**Answer:**
You can use the native Windows API to detect that a specific Windows form is open.
This can also be done in .NET.
You can search for something like [.net detect form open](https://duckduckgo.com/?q=.net+detect+form+open) to learn more.

You might also try to track the `DocumentChanged` event; Revit creates elements and a view with a persistent name ‘Modify type attributes’.
This name is probably language dependent, but Revit does not create any other events:

<center>
<img src="img/revitlookup_documentchanged.png" DocumentChanged monitor" title="RevitLookup DocumentChanged monitor" width="600"/> <!-- Pixel Height: 842 Pixel Width: 1,533 -->
</center>

**Response:**
I ended up using the built-in Revit API functionality to implement a small auxiliary class to solve it like this:

<pre><code class="language-cs">using System;
using System.Runtime.InteropServices;
using Autodesk.Revit.UI;

/// <summary>
/// Initializes a new instance of the <see cref="RevitWindowUtils"/> class.
/// </summary>
/// <param name="uiApplication"><see cref="UIApplication"/></param>
public class RevitWindowUtils(UIApplication uiApplication)
{
  private readonly IntPtr _mainWindowHandle = uiApplication.MainWindowHandle;

  [DllImport("user32.dll")]
  private static extern IntPtr GetActiveWindow();

  /// <summary>
  /// Is main Revit window active
  /// </summary>
  public bool IsMainWindowActive() => GetActiveWindow() == _mainWindowHandle;
}
</code></pre>

I create and store its static instance in the application class, and check it in IUpdater as follows:

<pre><code class="language-cs">public void Execute(UpdaterData data)
{
  if (!App.RevitWindowUtils.IsMainWindowActive())
    return;
</code></pre>

Many thanks to Aleksandr for this elegant solution.
