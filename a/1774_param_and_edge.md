<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Get Preview Image from Revit API
  https://forums.autodesk.com/t5/revit-api-forum/get-preview-image-from-revit-api/m-p/8985870

- How to bind a Shared Parameter to elements of both Type and Instance
  https://stackoverflow.com/questions/57653886/how-to-bind-a-shared-parameter-to-elements-of-both-type-and-instance


twitter:

Parameters and preview images in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...


linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Parameters and DirectShape Edges





####<a name="3"></a> Shared Parameter is Either Type or Instance

From the StackOverflow question
on [how to bind a shared parameter to both type and instance elements](https://stackoverflow.com/questions/57653886/how-to-bind-a-shared-parameter-to-elements-of-both-type-and-instance):

**Question:** I create a Shared Parameter programatically. This works well. I can also bind that parameter to different element types (categories) like Windows or Doors. However, once that is done, I struggle to create a new binding to for example a room which is not a family type, and should be bound to instances instead.

Is there any way to use ONE single shared parameter and bind that to both types and instances?

**Answer:** I believe not. A shared parameter is bound to either types or instances, not both.

**Response:** Thank you for answering so quickly.

I now tried to do it manually as well, through the UI, and got the message that it has already been added, so you unfortunately seems to be right. I am probably not the first to say this, but that is just veird behaviour. It forces me to create two parameters which have the completely identical defintion to add them to different categories.

**Answer:** Yes.

You must take into consideration that the shared parameters functionality was added very early in the Revit lifecycle and was completely oriented toward end user interaction. At the time, the Revit API did not even exist, and was not planned for.

Possibly your use case is weird, from a BIM point of view. I am not an expert on the user interface side of things, so I cannot say.

On the other hand, [extensible storage](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.23) was explicitly designed for API use.

If you do not explicitly need the end user or Revit to see and manipulate your data, e.g., for scheduling or other purposes, you should consider using extensible storage instead.

Neither Revit nor the end user will effectively see your extensible storage data.

Depending on your exact needs, this may be what you want or not.

####<a name="4"></a> Retrieve Element Parameters

<center>
<img src="img/pl_directshape_parameters.png" alt="DirectShape parameters" width="1425">
</center>

####<a name="4"></a> Hiding DirectShape Internal Face Edges

<center>
<img src="img/pl_volume_directshape.png" alt="DirectShape internal face edges" width="833">
</center>

<center>
<img src="img/pl_volume_dynamo.png" alt="Dynamo volumes" width="890">
</center>

<pre class="code">
</pre>


Hi philippe,
 
Glad it helps.
 
I would love to hear how it goes if you ever look deeper into it.
 
No, currently not planning on attending devcon.
 
cheers,
 
jeremy
 
From: Philippe Leefsma <philippe.leefsma@gmail.com>
Date: Wednesday, 28 August 2019 at 09:02
To: Jeremy Tammik <jeremy.tammik@autodesk.com>
Subject: Re: Revit API help - DirectShape + parameters
 
Alright great info thanks! 
 
It's not a problem for now, so I will let it like it is for the time being and look at BRepBuilder later on.
 
Will you be in DevCon Darmstad? See you there if you do ;)
 
Cheers,
Philippe.
 
On Wed, 28 Aug 2019 at 08:58, Jeremy Tammik <jeremy.tammik@autodesk.com> wrote:
Hi philippe,
 
It looks as if your shapes are created from separate triangular faces.
 
Would you like to mark some triangle edges so they are not displayed?
 
Since Dynamo can do this, there must be a way.
 
You can always debug ghe dynamo code and see for yourself what it is doing.
 
I would assume it is creating a complete polygonal face with all its edges in one single go.
 
I think this can be achieved using the TessellatedShapeBuilder class:
 
https://www.revitapidocs.com/2020/a144b0e3-c997-eac1-5c00-51c56d9e66f2.htm
 
No wrong, that one probably does generate triangulated faces like you have.
 
I believe the BRepBuilder enables you to create more complex faces with no edges in the middle:
 
https://www.revitapidocs.com/2020/94c1fef4-2933-ce67-9c2d-361cbf8a42b4.htm
 
cheers,
 
jeremy
 
From: Philippe Leefsma <philippe.leefsma@gmail.com>
Date: Wednesday, 28 August 2019 at 08:48
To: Jeremy Tammik <jeremy.tammik@autodesk.com>
Subject: Re: Revit API help - DirectShape + parameters
 
Hi Jeremy,
 
Right about the public questions, will do next time ;) Your code is not doing exactly what I was looking for, it copies all room parameters in the "Comment" section while I need to populate specific parameters... but luckily I found out that those parameters already exist on the newly created DirectShape, probably created by default as part of the project. So I dont have to create them, just set their value. I'm off the hook for now but some things in Revit API seem quite convoluted, at least for my BIM-ignorant mind...
 
while I have you... in the pictures below you can see Shapes created from dynamo (first pic) and Shapes created from my code (second pic). They look a bit different, can see tesselation on the second, so i'm wondering if you know the difference and if I can make mine look like the dynamo ones?
 
Thanks,
Philippe.
 

 

 
 
 
On Wed, 28 Aug 2019 at 07:51, Jeremy Tammik <jeremy.tammik@autodesk.com> wrote:
Hi philippe,
 
Nice to hear from you and glad to help.
 
Even though, even you can ask such a question in the public forum, so tat others can chip in and help, and the answer is also visible to others.
 
In this case, in fact, the answer is already included in the code you presumably already looked at:
 
https://github.com/jeremytammik/RoomVolumeDirectShape
 
look at these lines:
 
https://github.com/jeremytammik/RoomVolumeDirectShape/blob/master/RoomVolumeDirectShape/Command.cs#L695-L708
 
Look at the lines containing ‘[Pp]aram’.
 
Step through those in the debugger and you will see.
 
Cheers,
 
Jeremy
 
From: Philippe Leefsma <philippe.leefsma@gmail.com>
Date: Tuesday, 27 August 2019 at 14:52
To: Jeremy Tammik <jeremy.tammik@autodesk.com>
Subject: Revit API help - DirectShape + parameters
 
Hi Jeremy,
 
After stepping in the unknown for a few hours, I thought I could use a bit of your expertise.
 
The goal is to convert Revit rooms into DirestShape (done already with info from your blog), the next step is to copy the room parameters into the DirectShape object, in such a way they appear correctyl in the UI.
 
Below you can see a picture of some DirectShapes (Mass in english, "Volume" in the french version) created with dynamo. So I'm able to retrieve the room parameters, and I would like now to create the same in the DS I just created so they show up in that UI. Note the "comments" is already working, I just don't know how to set the others... 
 

 
Here is my code so far:
 
 
static
public void
ConvertToVolume(
 
Document
doc, Room
room, Boolean
fixShell)
 
{
 
try
 
{
 
var
roomShell = !fixShell
 
? room.ClosedShell.ToArray()
 
: GetRoomShell(room);
 
 

var
typeId = new
ElementId(BuiltInCategory.OST_Mass);
 
 

var
dsLib = DirectShapeLibrary.GetDirectShapeLibrary(doc);
 
 

var
dsType = DirectShapeType.Create(doc,
"Volume",
typeId);
 
dsType.SetShape(roomShell);
 
 

dsLib.AddDefinitionType("Volume",
dsType.Id);
 
 

var
shape = DirectShape.CreateElementInstance(
 
doc,
dsType.Id,
dsType.Category.Id,
"Volume",
 
Transform.Identity);
 
 

var
guid = doc.Application.ActiveAddInId.GetGUID();
 
 

var
TwopsParams = room.Parameters.Cast<Parameter>().Where(p
=> {
 
return
p.Definition.Name.StartsWith("Twops");
 
});
 
 

shape.ApplicationDataId =
room.UniqueId;
 
shape.Name =
"Volume for " +
room.Name;
 
shape.ApplicationId =
guid.ToString();
 
shape.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set(
 
"Volume for " +
room.Name +
" (Generated by Twops Addin)");
 
shape.SetTypeId(dsType.Id);
 
 

foreach (var
p
in TwopsParams)
 
{
 
// goal is to create in the shape,
 
// parameters with same value and Name than
 
// the ones in that TwopsParams collection ...
 
}
 
}
 
catch (Exception
ex)
 
{
 
Console.WriteLine(ex.Message);
 
}
 
}
 
 
Thanks,
Philippe.