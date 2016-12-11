<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

http://thebuildingcoder.typepad.com/blog/2014/04/determining-the-size-and-location-of-viewports-on-a-sheet.html#comment-3045289101

 #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

&ndash; 
...

-->

### Viewport Bring to Front

Joshua Lumley submitted
a [comment](http://thebuildingcoder.typepad.com/blog/2014/04/determining-the-size-and-location-of-viewports-on-a-sheet.html#comment-3045289101) on
the discussion on [Determining the Size and Location of Viewports on a Sheet](http://thebuildingcoder.typepad.com/blog/2014/04/determining-the-size-and-location-of-viewports-on-a-sheet.html),
suggesting:

> I made code that will change the draw order of a viewport (bring to front) in the cases when you need to use simplified duplicate of the same view for background (ceiling grid for example). Are you interested in it? I was hoping you could make a post and share it with the world (it has impressed my colleagues anyway).

> Here is the essence of the code:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;ViewElementID&nbsp;=&nbsp;ViewportList1[listBox1.SelectedIndex].ViewId;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;storetheposition&nbsp;=&nbsp;ViewportList1[listBox1.SelectedIndex].GetBoxCenter();
&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;storetypetypeID&nbsp;=&nbsp;ViewportList1[listBox1.SelectedIndex].GetTypeId();
 
&nbsp;&nbsp;Autodesk.Revit.DB.<span style="color:#2b91af;">View</span>&nbsp;pView&nbsp;=&nbsp;doc.ActiveView;&nbsp;Autodesk.Revit.DB.<span style="color:#2b91af;">Transaction</span>
&nbsp;&nbsp;&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Autodesk.Revit.DB.<span style="color:#2b91af;">Transaction</span>(&nbsp;doc,&nbsp;<span style="color:#a31515;">&quot;Form_2&quot;</span>&nbsp;);
&nbsp;&nbsp;t.Start();
&nbsp;&nbsp;ViewSheet1.DeleteViewport(&nbsp;ViewportList1[listBox1.SelectedIndex]&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">Viewport</span>&nbsp;vvp&nbsp;=&nbsp;<span style="color:#2b91af;">Viewport</span>.Create(&nbsp;doc,&nbsp;ViewSheet1.Id,&nbsp;ViewElementID,&nbsp;storetheposition&nbsp;);
&nbsp;&nbsp;t.Commit();</pre>
</pre>

As you can see, it is simply taking out a viewport and putting it back in the same place, because Revit paints viewports in the order they were created, so the most recently created viewport will come to the front.


####<a name="2"></a>Code Clean-Up

I cleaned up your code significantly like this:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Bring&nbsp;viewport&nbsp;to&nbsp;front&nbsp;by&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;deleting&nbsp;and&nbsp;recreating&nbsp;it.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">void</span>&nbsp;ViewportBringToFront(&nbsp;<span style="color:#2b91af;">ViewSheet</span>&nbsp;sheet,&nbsp;<span style="color:#2b91af;">Viewport</span>&nbsp;viewport&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;sheet.Document;
 
&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;viewId&nbsp;=&nbsp;viewport.ViewId;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;boxCenter&nbsp;=&nbsp;viewport.GetBoxCenter();
 
&nbsp;&nbsp;<span style="color:green;">//ElementId&nbsp;typeId&nbsp;=&nbsp;viewport.GetTypeId();</span>
&nbsp;&nbsp;<span style="color:green;">//View&nbsp;view&nbsp;=&nbsp;doc.ActiveView;</span>
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;Delete&nbsp;and&nbsp;Recreate&nbsp;Viewport&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;sheet.DeleteViewport(&nbsp;viewport&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Viewport</span>&nbsp;vvp&nbsp;=&nbsp;<span style="color:#2b91af;">Viewport</span>.Create(&nbsp;doc,&nbsp;sheet.Id,&nbsp;viewId,&nbsp;boxCenter&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
&nbsp;&nbsp;}
}
</pre>

Notes:

- Separate code from user interface implementation details. Isolate real functionality in separate methods. [Keep it simple](https://en.wikipedia.org/wiki/KISS_principle)!
- Encapsulate `Transaction` use in a separate `using` statement. Look at the explanations in The Building Coder topic group on [handling transactions and transaction groups](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.53).
- Commented out unused variables.

I added this method to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) in the
[module CmdViewsShowingElements.cs line 424-445](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdViewsShowingElements.cs#L424-L445).

