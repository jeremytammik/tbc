<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- String parameter filtering is retrieving false data
  https://forums.autodesk.com/t5/revit-api-forum/string-parameter-filtering-is-retrieving-false-data/m-p/10034687
  REVIT-172990 [Parameter filter with FilterStringContains returns false positive] original ticket
  REVIT-173253 [Parameter filter with FilterStringContains returns false positive] documentation fix

- https://forums.autodesk.com/t5/revit-api-forum/string-parameter-filtering-is-retrieving-false-data/m-p/10043934/highlight/false#M52973
  param_by_guid_includes_type.png
  Addendum by fair59

- OCR
  http://capture2text.sourceforge.net/
  open source Windows hotkey app: Windows + Q, mouse rectasgle, resulting text in clipboard
  Capture2Text is free and licensed under the terms of the GNU General Public License.
  supports almost a hundred target languages,
  multiple simultanaeous target languages, 
  immediate translation via Google translate and
  text-to-speech voice
  Capture2Text_conceptual_illustration.png 810

- [How to Be Productive, Feel Less Overwhelmed, and Get Things Done](https://www.freecodecamp.org/news/how-to-get-things-done-lessons-in-productivity) by Endy Austin

twitter:

A handy open source OCR tool, and filtered element collector with parameter filter also checks element type in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://autode.sk/paramfilter_etype

An important insight in using a filtered element collector with a parameter filter, a handy open source OCR tool and a few productivity tips
&ndash; Parameter filter also checks element type
&ndash; Capture2Text, a handy OCR tool
&ndash; Productivity tips...

linkedin:

A handy open source OCR tool, and filtered element collector with parameter filter also checks element type in the #RevitAPI

http://autode.sk/paramfilter_etype

An important insight in using a filtered element collector with a parameter filter, a handy open source OCR tool and a few productivity tips:

- Parameter filter also checks element type
- Capture2Text, a handy OCR tool
- Productivity tips...

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

### Parameter Filter Checking Element Type and OCR

Getting ready for a rainy weekend, here is an important insight in using a filtered element collector with a parameter filter, a handy open source OCR tool and a few productivity tips:

- [Parameter filter also checks element type](#2)
- [Parameter property also checks element type](#2.1)
- [Capture2Text, a handy OCR tool](#3)
- [Productivity tips](#4)

####<a name="2"></a> Parameter Filter Also Checks Element Type

We made an interesting discovery in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [string parameter filtering retrieving false data](https://forums.autodesk.com/t5/revit-api-forum/string-parameter-filtering-is-retrieving-false-data/m-p/10034687):

If the internal `ParameterValueProvider` class does not find the requested parameter from the element itself, will also try to read it from the element's type.

This undocumented behaviour can lead to some confusion and is difficult to fix perfectly in an efficient manner.

**Question:** I am trying to filter walls based on their "Fire Rating" parameter using the `FilterStringContains` evaluator.

However, this filter retrieves both walls satisfying the rule plus other walls that don't even contain that parameter!

This seems like an error to me.

Here is a [sample project with a macro](zip/TestFilterStringContains.rvt) providing a reproducible case using the following code:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>&nbsp;bip&nbsp;=&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.FIRE_RATING;
 
&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;pid&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;bip&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">ParameterValueProvider</span>&nbsp;provider
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ParameterValueProvider</span>(&nbsp;pid&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">FilterStringRuleEvaluator</span>&nbsp;evaluator&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilterStringContains</span>();
 
&nbsp;&nbsp;<span style="color:#2b91af;">FilterStringRule</span>&nbsp;rule&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilterStringRule</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;provider,&nbsp;evaluator,&nbsp;<span style="color:#a31515;">&quot;/&quot;</span>,&nbsp;<span style="color:blue;">false</span>&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>&nbsp;filter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>(&nbsp;rule&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;myWalls&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Walls&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;filter&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;false_positive_ids&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;();
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;wall&nbsp;<span style="color:blue;">in</span>&nbsp;myWalls&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;param&nbsp;=&nbsp;wall.get_Parameter(&nbsp;bip&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;param&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;false_positive_ids.Add(&nbsp;wall.Id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;s&nbsp;=&nbsp;<span style="color:blue;">string</span>.Join(&nbsp;<span style="color:#a31515;">&quot;,&nbsp;&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;false_positive_ids.Select&lt;<span style="color:#2b91af;">ElementId</span>,&nbsp;<span style="color:blue;">string</span>&gt;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;id&nbsp;=&gt;&nbsp;id.IntegerValue.ToString()&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>&nbsp;dlg&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">TaskDialog</span>(&nbsp;<span style="color:#a31515;">&quot;False&nbsp;Positives&quot;</span>&nbsp;);
&nbsp;&nbsp;dlg.MainInstruction&nbsp;=&nbsp;<span style="color:#a31515;">&quot;False&nbsp;filtered&nbsp;walls&nbsp;ids:&nbsp;&quot;</span>;
&nbsp;&nbsp;dlg.MainContent&nbsp;=&nbsp;s;
&nbsp;&nbsp;dlg.Show();
</pre>

This displays a message like this in the sample model, listing walls that are not even equipped with the target parameter, let alone have a value for it:

<center>
<img src="img/TestFilterStringContains_result_message.png" alt="Parameter filter returns false positives" title="Parameter filter returns false positives" width="442"/> <!-- 884 -->
</center>

What can I do to ignore walls that don't contain the parameter?

**Answer:** This was logged as *REVIT-172990 &ndash; Parameter filter with FilterStringContains returns false positive* for the development team to analyse.

You might be able to achieve the desired result by combining the FilterStringContains evaluator with other rules and filters.

For instance, you may be able to add something like 'is not empty' in addition to 'contains'.

The development team analysed the issue and found the reason for this behaviour.

If the internal method `ParameterValueProvider::getParameterValue()` cannot get the parameter from the element itself, will also try to get it from the element's type:

<pre class="code">
  if (m_elemOrSymbol == EOS_Symbol
    || err != ERR_SUCCESS || !oParameterValue)
  {
    ElementId typeId = pElement-&gt;getTypeId();
    if (validElementId(typeId))
    {
      const Element *pTypeElement
        = pElement-&gt;getDocument()-&gt;getElement(typeId);
        
      if (pTypeElement != NULL)
      {
        oParameterValue = pTypeElement-&gt;getParameterValue(
          m_parameter);
          
        err = getErrFromParameterValue(oParameterValue);
        if (err != ERR_SUCCESS)
        {
          return nullptr;
        }
      }
    }
  }
</pre>

Therefore, yes, you cannot find the parameter in the walls.

However, you can find the parameters in their wall type.

That's why you can still get those walls.

This behaviour has been in effect for a long time, so the development team is not thinking about changing it.

However, this information needs to be made clear in the documentation and knowledge base.

A new issue *REVIT-173253 &ndash; Parameter filter with FilterStringContains returns false positive* has been raised for the documentation team to document this behaviour. 

**Response:** I am going to accept your reply as an accepted answer but it is not as expected.

- I think this behaviour is not acceptable logically, even though it was in effect for a long time.
At least it should return the wall type itself rather than returning all walls under that type.
- Can it be modified to have a Boolean to search through the element's type or not?
Or, like the filtered element collector, e.g., `WhereElementIsElementType` and `WhereElementIsNotElementType`?

**Answer:** I agree that this behaviour defeats part of the purpose of the parameter filter.

If I were you, I would choose the simple solution of filtering for parameters that are guaranteed not to be present on the type.

**Answer 2:** I always preferred the functionality as it is now. There are various solutions out there for finding elements that rely on not having to separately look through the parameters on the type or know that a parameter is on a type or an instance.

Imagine you want to find all instances that have a certain type parameter, you would first have to filter for all types, filter those for the parameter match, then filter again for all instances that are of one of those types. This is already a common approach for a lot of cases where parameter filters can’t be used.

I thought it just saved a lot of work really, I see no need for a change. If you want to know the parameter is on the instance then put the results through a further filter. You can supply a list of ids to a further element filter at any time. If you want to know parameter is on the type, filter first for types. In general, I don’t see much use of Logical AND/OR Filters sometimes I wonder if their existence is known of? People seem to have other methods but again use: LogicalAndFilter with

- ParameterElementFilter and
<br/>ElementIsElementTypeFilter(Inverted = True)

ElementParameterFilter is a slow filter, so you should first use another quick filter before it.

It works like in the UI when you set up visibility filters, i.e., there is no distinction between type and instance parameters there. I imagine this is the internal system that this filter class leverages.

Later: When I consider further there is a bit of a hole in functionality there.

Since you are forced to get a list of elements and then rule out those that have a type parameter match not an instance parameter match, i.e., there are four cases:

Parameter with matching name and value is on: Type, Instance, Both, Neither.

We can only find with filter that it is on: Either or Neither.

So, the question is, how would we find Elements with a matching parameter on the Instance only?

After filtering with ElementParameterFilter, we would have to manually check via Linq perhaps.
We only care that it is or isn't on the instance, so that could simplify things.

**Answer 3:** Exactly what I meant by saying, defeats part of the purpose.

Thank you for fleshing it out in more detail.

Linq is super slow compared to the built-in parameter filter, and I see no other option to achieve the in-between distinctions.

Many thanks to Ameer and Richard for this very fruitful and illuminating discussion!

####<a name="2.1"></a> Parameter Property Also Checks Element Type

Our learning is never-ending.

I response to the above,
Frank [@Fair59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518) Aarssen added:

> I noticed the same behaviour for the Parameter properties of an Element.
Getting a parameter on (family) instances by GUID or Definition also returns the type-parameter:

<center>
<img src="img/param_by_guid_includes_type.png" alt="Parameter property by GUID and Definition" title="Parameter property by GUID and Definition" width="368"/> <!-- 368 -->
</center>

> Please update the documentation for these properties as well.

####<a name="3"></a> Capture2Text, a Handy OCR Tool

I happened upon a very handy OCR tool that looks pretty good:

[Capture2Text](http://capture2text.sourceforge.net) is
a free open source Windows desktop OCR application, licensed under the terms of the GNU General Public License.

It is implemented as a hotkey app, making it very minimalistic and efficient to use:

Click the Windows button + Q, mouse the source pixel rectangle to capture, and the resulting text is placed in the clipboard.

It can't get much more minimal or efficient than that, can it?

Besides that, Capture2Text supports:

- Almost a hundred target languages
- Multiple simultaneous target languages
- Immediate translation via Google translate
- Text-to-speech voice

<center>
<img src="img/Capture2Text_conceptual_illustration.png" alt="Capture2Text" title="Capture2Text" width="600"/> <!-- 810 -->
</center>

####<a name="4"></a> Productivity Tips

For more efficiency in personal and profession life in general, here are a couple of tips by
Endy Austin
on [how to be productive, feel less overwhelmed, and get things done](https://www.freecodecamp.org/news/how-to-get-things-done-lessons-in-productivity).

