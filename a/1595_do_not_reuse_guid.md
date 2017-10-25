<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://boostyourbim.wordpress.com/2017/10/13/quick-tip-change-those-guids/

Do Not Reuse an Existing GUID #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/noreuseguid

Several Revit API objects make use of a GUID to uniquely identify themselves.
When you copy and paste source code including any such GUID, you need to take care to replace the original GUID by your own one...

--->

### Do Not Reuse an Existing GUID

Several Revit API objects make use of
a [GUID](https://en.wikipedia.org/wiki/Universally_unique_identifier) to
uniquely identify themselves.

When you copy and paste source code including any such GUID, you need to take care to replace the original GUID by your own one.

You can easily create a new GUID using the Visual Studio GUID generator tool `guidgen.exe` and by other means, cf. the explanation on creating
an [add-in client id](http://thebuildingcoder.typepad.com/blog/2010/04/addin-manifest-and-guidize.html#4) for
a Revit add-in manifest.

Boost your BIM recently encountered and reported this issue in
its [quick tip &ndash; change those GUIDs!](https://boostyourbim.wordpress.com/2017/10/13/quick-tip-change-those-guids)

Here is another case illustrating the kind of problems that can occur if you simply copy and reuse an existing GUID:

**Question:** I recently updated to Revit 2018.2.
As a result, a custom dockable panel now throws an error at load.
This error was not encountered until after the update:

<pre>
Error Message:
Cannot register the same dockable pane ID more than once.
Parameter name: id
</pre> 

This is the code generating the error:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">DockablePaneId</span>&nbsp;id
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">DockablePaneId</span>(&nbsp;<span style="color:blue;">new</span>&nbsp;Guid(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;{D7C963CE-B7CA-426A-8D51-6E8254D21157}&quot;</span>&nbsp;)&nbsp;);
 
&nbsp;&nbsp;uiApp.RegisterDockablePane(&nbsp;id,&nbsp;<span style="color:#a31515;">&quot;Xyz&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;XyzPanelClass&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">IDockablePaneProvider</span>&nbsp;);
</pre>
 
What happened?

**Answer:** Several developers reported this issue.

I suspect people are copying code straight from
the [simpler dockable panel sample](http://thebuildingcoder.typepad.com/blog/2013/05/a-simpler-dockable-panel-sample.html),
not realizing they need to replace the GUID with their own one. 

Create your own GUID to replace the original one and the problem will be resolved.

<center>
<img src="img/guid_collision.png" alt="GUID collision" width="352"/>
</center>

