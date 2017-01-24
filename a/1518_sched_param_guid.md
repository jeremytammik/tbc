<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- Alexander Ignatovich (Александр Игнатович) 
  Elegant solution to get project parameters GUID directly for the building coder

- 12553492 [Getting parameter information from a schedule]
  http://forums.autodesk.com/t5/revit-api-forum/getting-parameter-information-from-a-schedule/m-p/6802850

#RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

Lots of information on, from and about schedule parameters, and a new elegant solution to a long-standing challenge
&ndash; Direct access to shared parameter GUID
&ndash; Getting parameter information from a schedule...

#AULondon, #UI, #innovation, #RevitAPI, @AutodeskRevit

Direct access to shared param GUID & param info from a schedule #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge http://bit.ly/paramguid

-->

### Schedule Parameter and Shared Parameter GUID

Lots of information on, from and about schedule parameters, and a new elegant solution to a long-standing challenge:

- [Direct access to shared parameter GUID](#2)
- [Getting parameter information from a schedule](#3)


####<a name="2"></a>Direct Access to Shared Parameter GUID

Alexander Ignatovich (Александр Игнатович) already shared several exciting solutions with us all in the past.

Today he faced a new problem.

As usual, he came up with an impressively direct solution to share.

The problem is rather old and known, and he found an elegant new way to solve it:

I wanted to get shared parameters GUIDs directly from project parameters.

Of course I found the solution demonstrated by 
the [shared project parameter GUID reporter](http://thebuildingcoder.typepad.com/blog/2015/12/shared-project-parameter-guid-reporter.html), where you need to attach the parameter to the project information (for instance parameters) and wall (for type parameters) categories.

I also found another blog post on [parameter of Revit API 30 &ndash; project parameter information](http://spiderinnet.typepad.com/blog/2011/05/parameter-of-revit-api-30-project-parameter-information.html), but this solution did not work for me, because all definitions in `doc.ParameterBindings` are `InternalDefinition` objects in Revit 2017.

I investigated further and found that `InternalDefinition` has an `Id`. I retrieved the corresponding database element from the document by this `Id`. I saw that `SharedParameterElement` is returned for shared project parameters and `ParameterElement` for non-shared project parameters. `SharedParameterElement` has a `GuidValue` property, which is exactly what I need.

The code:

<pre class="code">
&nbsp;&nbsp;[<span style="color:#2b91af;">Transaction</span>(&nbsp;<span style="color:#2b91af;">TransactionMode</span>.ReadOnly&nbsp;)]
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">CmdSharedParamGuids</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalCommand</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;uiapp&nbsp;=&nbsp;commandData.Application;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;uidoc&nbsp;=&nbsp;uiapp.ActiveUIDocument;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;bindingMap&nbsp;=&nbsp;doc.ParameterBindings;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;it&nbsp;=&nbsp;bindingMap.ForwardIterator();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;it.Reset();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">while</span>(&nbsp;it.MoveNext()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;definition&nbsp;=&nbsp;(<span style="color:#2b91af;">InternalDefinition</span>)&nbsp;it.Key;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;sharedParameterElement&nbsp;=&nbsp;doc.GetElement(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;definition.Id&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">SharedParameterElement</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;sharedParameterElement&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;non-shared&nbsp;parameter&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;definition.Name&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;shared&nbsp;parameter&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">$&quot;</span>{sharedParameterElement.GuidValue}<span style="color:#a31515;">&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;-&nbsp;{definition.Name}&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

Thank you very much for this elegant solution, Alexander!

Sweet and simple!

I am sure it will make many people very happy!

I therefore added it to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) 
in [release 2017.0.132.0](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2017.0.132.0).

We can make use of this right away to enhance the answer to the question below as well.



####<a name="3"></a>Getting Parameter Information from a Schedule

From
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread
on [getting parameter information from a schedule](http://forums.autodesk.com/t5/revit-api-forum/getting-parameter-information-from-a-schedule/m-p/6802850):

**Question:** The `ScheduleField` class has a property `ParameterId`, which is good.

Now I would like to know more about this parameter:

1. The name of the parameter.
2. If it is an instance or type parameter.
3. If it is a shared or built-in parameter.
4. The parameter Unit.
5. GUID of the parameter if it is shared.

I think I can get 1 and 2 working, but pretty much clueless about 3, 4 and 5.

**Answer:** Before anything else, I must ask you:

Have you installed [RevitLookup](https://github.com/jeremytammik/RevitLookup) and are you using it on a regular basis in your Revit database exploration?

With that tool, you can interactively snoop the database.

In this case, you could grab one of those ParameterId values, use the built-in Revit *Select by Id* command in the *Manage* tab to select the corresponding database element, regardless of whether it has a graphical representation or not, and interactively explore its nature, contents, parameters, relationships with other elements, etc.

That will probably answer your question.

It is statically compiled, however, so it mainly displays properties. It does not dynamically evaluate all methods available on all classes.

For that, you can use other,
even [more powerful and interactive database exploration tools](http://thebuildingcoder.typepad.com/blog/2013/11/intimate-revit-database-exploration-with-the-python-shell.html).

Also, my discussion
on [how to research to find a Revit API solution](http://thebuildingcoder.typepad.com/blog/2017/01/virtues-of-reproduction-research-mep-settings-ontology.html#3) might
come in handy for you at this point.

**Response:** I have installed RevitLookup for Revit 2015 (the version I'm developing as our development needs to be backward compatible with our current projects).

RevitLookup is a great tool that can save hundreds of hours of time on browsing the "watch" in the debug mode, which I have been using.

However, I still can't find the field parameter definitions (shared or built-in) for the `ScheduleField` class. I understand the "viewschedule" and "schedulefield" are different classes. I suspect this may have to do with Revit API's limitation.

By the way, it seems RevitLookup defines a ribbon panel in the 'Add-ins' tab &ndash; however, I can't see the 'Manage Tab'; am I missing something here?

**Answer:**

1. name of the parameter:

<pre class="code">
  Name = Field.GetName()
</pre>

2. Type or Instance?

Probably only a definitive answer for a schedule of 1 System Family Category. Shared Parameters in User Created Families can be both Type and Instance (in different families).

3. Shared or BuiltIn?

- Field.ParameterId < -1 : BuiltInParameter &ndash; Field.ParameterId == BuiltInParameter value
- Field.ParameterId > 0 : SharedParameter
- Field.ParameterId = -1 : miscellaneous (calculated value, percentage)

Shared Parameter:

<pre class="code">
  SharedParameterElement shElem = doc.GetElement(
    Field.ParameterId) as SharedParameterElement;
</pre>

You can find the answer to 4 and 5 in `shElem.Definition` .

Built-in Parameter:

You need an element (Elem) to get access to a BuiltInParameter, so you need to have a non-empty schedule.

<pre class="code">
  Parameter par = Elem.get_Parameter(
    (BuiltInParameter) Field.ParameterId.IntegerValue );
</pre>

You can find the answer to 4 in `par.Definition`.

**Response:** Very nice answers, thanks a lot.

Regarding 2, I guess I have to use the `FamilyManager` to open the families to find out if the shared parameter is a 'Type' or 'Instance' one.

**Answer:** More on *Select by Id*:

[Select element by id](https://knowledge.autodesk.com/support/revit-products/learn-explore/caas/CloudHelp/cloudhelp/2017/ENU/Revit-Troubleshooting/files/GUID-2B1CC22C-CB1F-45DA-B57B-62C36013D9E0-htm.html) is
part of the standard end user interface:

- Help entry on [Select Elements by ID](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-2B1CC22C-CB1F-45DA-B57B-62C36013D9E0)
- 101-second YouTube video on [Selecting Elements Using the Element ID](https://www.youtube.com/watch?v=prv8nGrU56o):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/prv8nGrU56o?rel=0" frameborder="0" allowfullscreen></iframe>
</center>
 
As said, it comes in handy for using RevitLookup to snoop element data and properties, since it can be used on invisible elements that cannot be selected in any other way as well.
 
**Response:** I found the post you mentioned and tried; it works well that I can find the shared parameter visually on the Revit property panel even it is a 'hidden' element; that's quite cool. :)
