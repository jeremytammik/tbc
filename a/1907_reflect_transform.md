<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

 #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

###

####<a name="2"></a>

<center>
<img src="img/.png" alt="" title="" width="401"/> <!-- 802 -->
</center>

####<a name="3"></a>

 ABertzouanis9F98Y 71 Views, 3 Replies
‎05-21-2021 09:17 AM 
GetTransform() does not include reflection into the transformation
 
Instance.GetTransform() method..

https://www.revitapidocs.com/2015/50aa275d-031e-ce19-9cfd-18a7a341ed19.htm

..does not include reflection (the family mirrored property https://www.revitapidocs.com/2015/20ab2f32-e3ca-8173-aac3-a03e998fd0ab.htm) into its transformation. Below a family instance which is mirrored and outputs the equivalent GetTansform() values..

Transformation Matrix2.PNG

Here is the python code:

import sys
import clr
clr.AddReference('ProtoGeometry')
from Autodesk.DesignScript.Geometry import *
data= UnwrapElement(IN[0])

output=[]
for i in data:
	output.append(i.Location.Point)
	output.append(i.GetTransform().BasisX)
	output.append(i.GetTransform().BasisY)
	output.append(i.GetTransform().BasisZ)
	output.append("")
OUT = output

Tags (0)
Add tags
Report
3 REPLIES 
Sort: 
MESSAGE 2 OF 4
jeremy.tammik
 Employee jeremy.tammik in reply to: ABertzouanis9F98Y
‎05-22-2021 09:10 AM 
Yes, true. Afaik, that is expected and intentional.

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder
Tags (0)
Add tags
Report
MESSAGE 3 OF 4
ABertzouanis9F98Y
 Observer ABertzouanis9F98Y in reply to: jeremy.tammik
‎05-24-2021 01:20 AM 
 
@jeremy.tammik many thanks for your answer.

However, from a mathematical perspective it should not be expected. Below wikipedia's article that very clearly shows that element which is reflected on the x axis should have a different transformation matrix.

Transformation matrix - Wikipedia 

transformation Matrix3.PNG

Can you please share any explanation and why this intentional for Revit? Any link or further documentation would be much appreciated.

Regards,

Tags (0)
Add tags
Report
MESSAGE 4 OF 4
RPTHOMAS108
 Advisor RPTHOMAS108 in reply to: ABertzouanis9F98Y
‎05-24-2021 04:29 AM 
I found results that indicate Revit uses a combination of reflection and rotation for the various operations.

210524a.PNG

One thing that stands out is the difference between horizontal double flip control and mirror command about same axis (noted red). These operations are almost identical apart from the horizontal one that results in opposite facing and handed state. Graphically it appears the same but not according to facing/handed orientation.

It has been noted previously that single flip control is more like rotating rather than mirroring (it doesn't result in reflected geometry). We see by transform that it is reflected but facing/handed state is also set to true.

Generally I think of the facing/handed state as being an internal to the family state i.e. the internal geometry may be reflected but the family itself isn't (unless it is by transform).

You probably need to look at flip state/rotation and transform to get a definitive idea of the situation. These controls long ago I believe were introduced for doors, which side they are hung and swing direction. As they started being used for other things the ambiguities crept in i.e. double negative (same ultimate representation but two definitions for it).

####<a name="4"></a>

####<a name="5"></a> The SetGeometryCurve OverrideJoins Argument

<pre class="code">

</pre>

Thank you, , for pointing this out!
