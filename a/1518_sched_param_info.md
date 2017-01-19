<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 12553492 [Getting parameter information from a schedule]
  http://forums.autodesk.com/t5/revit-api-forum/getting-parameter-information-from-a-schedule/m-p/6802850

#RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

&ndash; ...

#AULondon, #UI, #innovation, #RevitAPI, @AutodeskRevit bit.ly/2j7Sxkb

-->

### Retrieving Schedule Parameter Information


- [](#2)


####<a name="2"></a>

**Question:**

The "ScheduleField" class has a property "ParameterId", which is good.

Now I would like to know more about the this parameter:

1. The name of the parameter,
2. If it is an instance or type parameter,
3. If it is a shared or built-in parameter,
4. The parameter Unit,
5. GUID of the parameter if it is shared.

I think I can get 1 and 2 working, but pretty much clueless about 3, 4 and 5.

**Answer:**

Before anything else, I must ask you:

Have you installed [RevitLookup](https://github.com/jeremytammik/RevitLookup) and are you using it on a regular basis in your Revit database exploration?

If the answer is 'no', I suggest we terminate this discussion right here and now until you have installed it and tested using it for your situation:

With that tool, you can interactively snoop the database.

In this case, you could grab one of those ParameterId values, use the Revit API Manage tab to select the corresponding database element, regardless of whether it has a graphical representation or not, and interactively explore its nature, contents, parameters, relationships with other elements, etc.

That will probably answer your question right then and there.

It is statically compiled, however, so it mainly displays properties. It does not dynamically evaluate all methods available on all classes.

For that, you can use other,
even [more powerful and interactive database exploration tools](http://thebuildingcoder.typepad.com/blog/2013/11/intimate-revit-database-exploration-with-the-python-shell.html).

Also, my discussion
on [How to Research to Find a Revit API Solution](http://thebuildingcoder.typepad.com/blog/2017/01/virtues-of-reproduction-research-mep-settings-ontology.html#3) might
come in handy for you at this point.

**Response:** 

I have installed RevitLookup for Revit 2015 (the version I'm developing as our development needs to be backward compatible with our current projects).

RevitLookup is a great tool that can save hundreds hours of time on browsing the "watch" in the debug mode, which I have been using.

However, I still can't find the field parameter definitions (shared or builtin) for the "schedulefield" class. I understand the "viewschedule" and "schedulefield" are different classes. I suspect this may have to do with Revit API's limitation.

By the way it seems RevitLookup is a ribbon panel in the "Add-ins" tab, I can't see "API Manage Tab", am I missing something here?

**Answer:**

1  Name =  Field.GetName()

2  Type or Instance?  Probably only a definitive answer for a schedule of 1 System Family Category. Shared Parameters in User Created Families can be both Type and Instance ( in different families)

3  Shared or BuiltIn? 
Field.ParameterId < -1 : BuiltInParameter     Field.ParameterId == BuiltInParameter value
Field.ParameterId >0:    SharedParameter
Field.ParameterId = -1: miscellaneous ( calculated value, percentage)
Shared Parameter:
SharedParameterElement shElem = doc.GetElement(Field.ParameterId) as SharedParameterElement
You can find the answer to 4 and 5 in shElem.Definition 

BuiltParameter:
You need a element (Elem) to get access to a BuiltInParameter, so you need to have a non empty schedule.
Parameter par = Elem.get_Parameter((BuiltInParameter) Field.ParameterId.IntegerValue);
You can find the answer to 4 in par.Definition

**Response:** 

Very nice answers, thanks a lot.

Regarding 2, I guess I have to use the `FamilyManager` to open the families to find out if the shared parameter is a "Type" or "Instance" one.

**Answer:**

[Select element by id]() is part of the standard end user interface:
 
https://knowledge.autodesk.com/support/revit-products/learn-explore/caas/CloudHelp/cloudhelp/2015/EN...
 
As said, it comes in handy for using RevitLookup to snoop element data and properties, since it can be used on invisible elements that cannot be selected in any other way as well.
 
**Response:** 
 
I have found the post you mentioned and tried, it works well that I can find the the shared parameter visually on the Revit property panel even it is a "hidden" element, that's quite cool. :)

<center>
<img src="img/.png" alt="" width="500"/>
</center>




####<a name="3"></a>


<pre class="code">
</pre>

