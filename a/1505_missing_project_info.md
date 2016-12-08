<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

Restoring a Missing Project Information Element #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

Some people have recently reported that they encountered models lacking the <code>ProjectInfo</code> project information singleton element.
Apparently, it was possible in previous versions of Revit for a faulty or malicious add-in to simply delete this element.
That obviously causes problems for other add-ins and Revit itself, who rely on its presence.
Luckily, it is not hard to fix.
Here is the latest discussion addressing this issue...

-->

### Restoring a Missing Project Information Element

Some people have recently reported that they encountered models lacking the `ProjectInfo` [project information singleton element](http://www.revitapidocs.com/2017/e90b12f3-9bf4-f536-3556-c9944cbf9f38.htm).

Apparently, it was possible in previous versions of Revit for a faulty or malicious add-in to simply delete this element.

That obviously causes problems for other add-ins and Revit itself, who rely on its presence.

Luckily, it is not hard to fix.

Here is the latest discussion addressing this issue:

**Question:** I encountered models missing Project Information, causing errors to be thrown.

<center>
<img src="img/missing_project_info.png" alt="Missing Project Information" width="500"/>
</center>

Can you provide any guidance on a potential fix for this?

Can we perform a Transfer Project Standards like process to copy the project information in from another model?

**Answer:** You can use the copy and paste API and
the [CopyElements method](http://www.revitapidocs.com/2017/b22df8f6-3fa3-e177-ffa5-ba6c639fb3dc.htm) to
copy in the project information element from some other intact RVT file.

The file can also be fixed via [transfer project standards](https://knowledge.autodesk.com/support/revit-products/troubleshooting/caas/sfdcarticles/sfdcarticles/Project-information-button-is-inactive-in-Revit.html).

We believe this problem to be fixed in Revit 2017 and later version, because API applications are now prevented from (hopefully accidentally) deleting the element.

**Response:** Thanks.

I have been successful in using the copy-paste method to automate the process to circumvent the errors and add the missing Project Information.
 
Thanks again for the help and suggestions!
