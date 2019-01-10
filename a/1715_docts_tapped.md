<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- 14918470 [Find all ducts that have been tapped into]
  https://forums.autodesk.com/t5/revit-api-forum/find-all-ducts-that-have-been-tapped-into/m-p/8485269

the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

&ndash; 
...

-->

###

#### <a name="2"></a>

**Question:**

<pre class="code">
</pre>

**Answer:**

the [Revit API getting started material](https://thebuildingcoder.typepad.com/blog/about-the-author.html#2) and

from [The Building Coder](https://thebuildingcoder.typepad.com) discussion
on [creating face wall and mass floor
](https://thebuildingcoder.typepad.com/blog/2017/12/creating-face-wall-and-mass-floor.html):

**Response:**

the [Revit online help](http://help.autodesk.com/view/RVT/2019/ENU) &gt;
Revit Developer's Guide
&gt; [Revit API Developers Guide](http://help.autodesk.com/view/RVT/2019/ENU/?guid=Revit_API_Revit_API_Developers_Guide_html).

#### <a name="3"></a>

#### <a name="4"></a>

<center>
<img src="img/.png" alt="" width="600">
</center>

Thank you very much,

14918470 [Find all ducts that have been tapped into]

Jared Wilson, jared.wilson@benham.com, Jared Wilson

https://forums.autodesk.com/t5/revit-api-forum/find-all-ducts-that-have-been-tapped-into/m-p/8485269

wils02  17 Views, 0 Replies 2018-12-23
‎2018-12-23 04:52 AM  
Find all ducts that have been tapped into

I have been going back and forth on how to approach this task, and I have thought myself into a rut. So here is my question...How do I retrieve ALL instances of where a duct has been tapped into by another duct?

The most important aspect of this challenge is I DO NOT want to do this by filtering systems in a collector. Lets assume for this task, none of the ductwork is actually assigned a system, they are all undefined.

BONUS POINTS: I also ONLY want the largest instance tapped for each duct. I.e. if a single duct has eight taps, I only want to add the largest tap to my list or collector or whatever.

Sincerely,

J

This topic was escalated to Salesforce Case 14918470 on 12-22-2018 by an automatic escalation. Case status: Open.

-----------------------------------------------------------------------
jeremytammik  in reply to:  Rankwils02
‎2018-12-23 04:59 PM  
Re: Find all ducts that have been tapped into

Dear Jared,

Thank you for your query.

Please excuse my naivety, but this seems quite simple to me.

Or maybe I should say, fundamental, my dear Wilson.

However, as far as I know there is no way to retrieve elements from a Revit database except by using a filtered element collector.

Why in heaven's name would you want to avoid using that?

How about this approach?

Filter for all ducts in the model.
On each duct, grab all its connectors.
If there are more than two, you have a duct with a tap, so ignore all others.
That gives you your list of ducts with taps.
Grab the connector with the largest whatever-you-want, width, height, diameter, flow, you name it.

Bob's your uncle.

I hope this helps.

Best regards,

Jeremy

-----------------------------------------------------------------------
wils02  in reply to:  Rankjeremytammik
‎2018-12-26 02:24 PM  
Re: Find all ducts that have been tapped into

Now we are getting somewhere, but my pseudo-code translator is not as # as others.

I DEFINITELY wanted to use a filtered element collector. After your billion or so posts about it, I know it's powerful. I only meant, I don't want to rely on filtering by systems for this code.

So far so good, I got the ducts in my document, and have the shape of what you said, but now for my next question.

I want to get all of the ducts with more than two connectors.
I want to find the diameter or height of the largest connector, excluding its own two.
Do something with this information.
Make a change to the duct in question.

Where do I put my transaction? Should I search and filter the document and create some sort of paired list: largest connector and associated duct, and then inside my transaction, edit the duct? Do I wrap everything numbered above in the transaction?

Jared

-----------------------------------------------------------------------
Yulong.Jin
2018-12-27 04:02 AM  
Re: Find all ducts that have been tapped into

Hi:

Filtering elements from Revit DB requires no open transaction. And neither does the task of accessing parameters of an element or connector.

You can either wrap your task 1,2 into an open transaction or not.

For you task 3, you wanna do something with this information. I'm not sure what kind of 'something' you are gonna do, so if you want to 'write some parameters into an exisiting element', you need an STARTED transaction. But if you only want to do some calculations, transations is not necessary.

For your task 4, yep, you definitily need an STARTED transaction, because you are about to change the Revit DB.

For my experience, requiring an open transaction or not depends on the procedure of UI interaction, for example, modal or modeless dialog you are using, needing the user to pick elements or not. If all your task 1-4 are in the same ExternalCommand, I tend to wrap them into one transaction, and start it at the beginning of IExternalCommand.Execute() method.

Furthermore, suppose you do someting in your task 3 to change the document, you can start an transation and commit it. Then you proceed to handle task 4, you start another new transaction and commit it. This will add 2 records in Undo list (Ctrl + Z).

If you wrap everything into 1 single transation and commit it, there will be only 1 new record in Undo list.

-----------------------------------------------------------------------
wils02
2018-12-27 07:01 PM  
Re: Find all ducts that have been tapped into

@Yulong.Jin and @jeremytammik

We did it! Now to pretty it up...

First, I'll explain. Sometimes in Revit, using the duct sizing tool or sizing manually, the branch ducts can end up being larger than the main duct they are tapping into. Revit doesn't do a great job bringing it to your attention, and if you do not think about it as you are working on a project, it's easy to forget about. So, you end up with questions about duct sizes when people go to build your system(s). We can't have that, especially when it can be easily found and resolved with a simple script.

With your help, we made a routine that finds all instances of duct with taps, finds the largest tap, and sizes the main duct 2" larger than the largest tap. There will probably be a few revisions I'm sure, but overall I am very happy and excited to use it on the next large project (which I should be working on at the moment but I can't help myself).

Even though I have a solution, it is not elegant by any means, and that bothers me. Please, please, please give me feedback regarding improvements I could make to simplify/clean up the code! Any and all is welcome. Attached is the source code.

Jared

TapResize.txt

/a/case/sfdc/14918470/attach/tap_resize.txt

-----------------------------------------------------------------------
wils02
2018-12-27 09:12 PM  
Re: Find all ducts that have been tapped into

Woops...haha no I do NOT have it.

I believe I am wrong with my looping through connectors. If I use the Snoop tool on my main duct and navigate to the connector manager; I have a connector set. This is where I went awry. If I open this connector set, I see the NUMBER of connectors for this main duct, but they ALL have the shape of the main duct which I was not expecting. After I clicked around for a while, I saw the "AllRefs" property of each connector brings me to another connector set. THIS is the one that shows me actual shape of each connector I choose.

After I selected the main duct in my project, the following is my click path with the Revit Snoop tool:

"Snoop Current Selection" --> "MEPCurve.Properties.ConnectorManager - Connector Manager" --> "Connector Set" --> (At this point I have a list of all the connectors associated with the main duct) "Connector.Properties - AllRefs"

My questions is, how do I actually access the shape property found under the AllRefs property?

Sorry for the confusion with the last post, but I would appreciate any help with this one before revising the code obviously.

Jared

-----------------------------------------------------------------------
Yulong.Jin
2018-12-28 07:42 AM  
Re: Find all ducts that have been tapped into

Hi @wils02:

In your last post, your prupose is to get the SHAPE of a connector, isn't it?

If so, there is a property of Connector class named as Shape, which is an enum, and it can tell you what shape the connector is (Rectangular, Oval, Round, Invalid). No matter you get the connector instance from Duct.ConnectorManager, or AllRefs, the Shape property is always accessible.

Please note that Tap connector on a duct is of type 'Curve', the normal two connectors at both ends of a duct are of type 'End'.

-----------------------------------------------------------------------
Yulong.Jin
2018-12-28 07:45 AM  
Re: Find all ducts that have been tapped into

Oops @wils02:

Sorry for my last reply. I wrote the words before I read you .txt attachment.

In you code, you have achieved all you want I guess.

And question is? I hope you have already make it.

-----------------------------------------------------------------------
jeremytammik  in reply to:  Rankwils02
‎2018-12-28 01:08 PM  
Re: Find all ducts that have been tapped into

Dear Jared,

You say:

> My questions is, how do I actually access the shape property found under the AllRefs property?

My answer is:

The same as always: RTFM:

https://apidocs.co/apps/revit/2019/bfd0a83e-c6a4-cec6-8428-b5b8b4357ee5.htm

I hope this helps.

Oh, I looked at your sample code in the text document too now, and see that you are already reading the AllRefs and Shape properties there.

So, to repeat Yulong's question:

Have you achieved all you wanted?

Would you like to provide a complete minimal reproducible case with steps to follow to show how your code works?

https://thebuildingcoder.typepad.com/blog/about-the-author.html#1b

Might be useful for others to take a look... thank you!

Best regards and Happy New Year!

Jeremy

-----------------------------------------------------------------------
wils02  in reply to:  Rankjeremytammik
‎2018-12-28 04:29 PM  
Re: Find all ducts that have been tapped into  
@Yulong.Jin and @jeremytammik

Attached is my testing project w/ the application-level macro. I managed to make a few simplifications. A few tweaks I want to make are:

Better exception handling or some other way to determine the main duct's shape.
Rather than using a blanket duct collector, collect all ducts that are physically connected together and size them in the direction of flow.
A slightly better UI rather than a black-box approach.
This will do for now, and I do hope people find it useful! On to the next macro...hear from you guys soon.

Jared

TapResize test environment.rvt

/a/case/sfdc/14918470/attach/tap_resize_test_environment.rvt

