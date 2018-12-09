<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- [Automatic Creation of Void Extrusion Element/ Retrieve Cut Area from element](https://forums.autodesk.com/t5/revit-api-forum/automatic-creation-of-void-extrusion-element-retrieve-cut-area/m-p/8451742)

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

&ndash; 
...

-->

### Fire Rating Zone Intersection

Last week, we looked at some 3D intersection filtering tasks.

Now, let's tackle a 2D intersection one.

Actually, it was originally raised as a 3D intersection task, in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [automatic creation of void extrusion and retrieval of cut area from element](https://forums.autodesk.com/t5/revit-api-forum/automatic-creation-of-void-extrusion-element-retrieve-cut-area/m-p/8451742):

<center>
<img src="img/fire_rating_zone_inters.png" alt="Fire rating zone intersection and subdivision" width="1104">
</center>


Jack94HNN  80 Views, 8 Replies 2018-11-29
‎2018-11-29 10:53 PM  
 Automatic Creation of Void Extrusion Element/ Retrieve Cut Area from element  
Hello Everybody,

 

I’m finding it difficult to find any informative material on a subject in the API that I was hoping might have already been explored.

 

Specifically I was hoping there would be an instance where a “model in place” void element is programmatically created in the model using given parameters for shape and dimensions.

 

For my purposes I was hoping to find a way to use an existing Solid Extrusion Element’s Geometry to define the geometry of this void as I create it.

 

The overall point would be to:

 

Have a Transparent extrusion hidden in the model that represents the Property Boundary for the Site of the Building, and the area 900 mm in from it (like a 3D cubic Object that represents the legal fire rating zone).
Create a list of elements from the exterior of the building that need to be tested for needing a fire proof layer; (such as Walls, Soffits ETC).


Then in the same transaction:

create a void Extrusion element the same shape and size as the Solid extrusion,
Iterate through each of the elements in need of testing and attempt to cut them with the void,
if the cut is successful record the cut area of the element that is being tested,
Keep that as the area of that element that needs to be fire rated,
Then having tested all the relevant elements Rolling back the transaction.

 

I Don’t Know if that is even possible, but I thought it might allow me to automatically store the required fire rated area for each element. All the drafts person has to do is draw in a Solid extrusion 900 mm wide and as tall as the building based on the plan of subdivision and hide it in the model.

 

I would be really Stoked for any advice, references or links that seem relevant to the task. it would be much appreciated.

 

I have attached an image that may help with visualizing the problem.

 

Regards,

Jack.

 Solved by jeremytammik.	Go to Solution.

 
Descriptive Image.png
 
Tags:Cut Areacut geometryVoid Extrusion Creation
Add tags
Report
MESSAGE 2 OF 9
jeremytammik
  Employee jeremytammik  in reply to:  RankJack94HNN
‎2018-12-02 09:22 AM  
Re: Automatic Creation of Void Extrusion Element/ Retrieve Cut Area from element  
Here are some discussions dealing wit various aspects of voids:

 

http://thebuildingcoder.typepad.com/blog/2011/06/boolean-operations-and-instancevoidcututils.html

 

http://thebuildingcoder.typepad.com/blog/2010/07/beam-maker-using-a-void-extrusion-to-cut.html

 

http://thebuildingcoder.typepad.com/blog/2014/04/instancevoidcututils-and-need-for-regeneration.html

 

http://thebuildingcoder.typepad.com/blog/2014/10/brussels-hackathon-and-determining-pipe-wall-thickn...

 

http://thebuildingcoder.typepad.com/blog/2017/03/wta-mech-and-ttt-for-provision-for-voids.html

 

http://thebuildingcoder.typepad.com/blog/2017/06/findinserts-determines-void-instances-cutting-a-flo...

 

Cheers,

 

Jeremy

 

 

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Add tags
Report
MESSAGE 3 OF 9
jeremytammik
  Employee jeremytammik  in reply to:  RankJack94HNN
‎2018-12-02 09:33 AM  
 Re: Automatic Creation of Void Extrusion Element/ Retrieve Cut Area from element
Looking at your descriptive image, the problem seems quite simple to me.

 

You have a certain area of interest, and certain elements that partially intersect the volume above it.

 

Instead of determining the intersecting volume, I would suggest retaining the area of interest in 2D, in the XY plane, and projecting the elements onto the XY plane as well.

 

Once you have everything in 2D in the XY plane, you have reduced the problem to a simple 2D Boolean intersection task.

 

That can be perfectly and completely addressed using a library to compute Boolean Operations for 2D Polygons:

 

https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.2

 

Cheers,

 

Jeremy

 

 

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Add tags
Report
MESSAGE 4 OF 9
Jack94HNN
  Community Visitor Jack94HNN  in reply to:  Rankjeremytammik
‎2018-12-04 01:43 AM  
Re: Automatic Creation of Void Extrusion Element/ Retrieve Cut Area from element  
Hi Jeremy,

 

That  RvtClipper.zip  was a very useful resource thank you. i had a play with the clipperlib on a previous task but did not consider putting it to use here.

 

I am currently working on adapting it to my aim and will update this post as soon as i think I've figured it out.

 

Thanks,

Jack.

Add tags
Report
MESSAGE 5 OF 9
jeremytammik
  Employee jeremytammik  in reply to:  RankJack94HNN
‎2018-12-04 08:47 AM  
Re: Automatic Creation of Void Extrusion Element/ Retrieve Cut Area from element
Dear Jack,

 

great! I am very glad it helped. I think this can be used to create a very optimal solution to the ask you describe. I look forward very much to hearing what you come up with.

 

Cheers,

 

Jeremy

 

 

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Add tags
Report
MESSAGE 6 OF 9
Jack94HNN
  Community Visitor Jack94HNN  in reply to:  Rankjeremytammik
‎2018-12-05 10:51 PM  
Re: Automatic Creation of Void Extrusion Element/ Retrieve Cut Area from element
Dear Jeremy,

 

Thanks again, that worked out really well.

In the end I swapped out the RvtClipper Result Execute() method to be a List<CurveArray> Execute() Method, 

and substituted the doc.selection based component for 2  element parameters that i pass when i call it.

(Being the boundary element and whichever element I am currently testing for an intersection as i iterate through them) 

I then Loop through the resulting CurveArrays that it returns and:

Subject the Curve Arrays to Your SortCurvesContiguous() Method

Convert them to CurveLoops,

Check if the CurveLoop is Open,

If it is; I take the start of the 1st Curve and the end of the last Curve and add a new Curve to the loop using these vertices as its start and end,

Next I Convert the loop back to the Array,

I then Apply your SortCurvesContiguous() Method again just to be sure,

Then lastly just create a floor of a previously prepared type named "Fire Rated Layer" using these Curves that sits just on top of the soffit that was being tested.

 

All in all, I'm Chuffed with the outcome.

 

All the best and Thank You,

    Jack.

 

 

Add tags
Report
MESSAGE 7 OF 9
jeremytammik
  Employee jeremytammik  in reply to:  RankJack94HNN
‎2018-12-06 07:47 AM  
Re: Automatic Creation of Void Extrusion Element/ Retrieve Cut Area from element
Dear Jack,

 

I am really glad to hear it worked out so well and you are so happy with the result.

 

Thank you also for the overview of the steps of your approach.

 

If you would like to share the source code and a minimal test model so that I can reproduce it my end, I think this would warrant a blog post of its own.

  

Cheers,

 

Jeremy

 

 

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Add tags
Report
MESSAGE 8 OF 9
Jack94HNN
  Community Visitor Jack94HNN  in reply to:  Rankjeremytammik
‎2018-12-07 03:09 AM  
Re: Automatic Creation of Void Extrusion Element/ Retrieve Cut Area from element
I apologize for the delayed reply,

 

I would be glad to do so, I wonder though;

as i have utilized this function as a component of a bigger two part method - and have to some degree intertwined the two functions, would you still like to have it.

further more, i have developed it to be reliant on several other methods so it may seem somewhat verbose as a Sample.

 

With all this taken into account, i wasn't sure if you would like to retract your offer as the sample will be quite large and not entirely focused on the topics touched on in this conversation.

 

I will outline the whole thing so you may consider.

 

The overall method now takes all of the soffits of a specific type (Named "Eave") and:

1. spends some time Breaking them into segments that replace the initial elements.

Then

2. Collects and subjects the new elements to the boundary overlap Query, creating the fire rating layer elements.

 

Step one not being entirely relevant to step 2 but contingent non the less.

 

Up To you Jeremy. 

 

Regards,

  Jack.

 

 

 

 

 

 

Add tags
Report
MESSAGE 9 OF 9
Jack94HNN
  Community Visitor Jack94HNN  in reply to:  Rankjeremytammik
‎2018-12-07 05:14 AM  
Re: Automatic Creation of Void Extrusion Element/ Retrieve Cut Area from element  
Dear Jeremy,

 

Having Gone back through and doing a bit of house keeping, its not as bad as i had apprehended. 

please forgive my habit of notating via regions, i have a bit of a thing for abstraction whilst i work.

 

it is all attached below.

 

I hope it reads ok.

Thanks,

    Jack.
    
[soffit subdivision and fire rating sample](zip/jb_soffit_subdivision_and_fire_rating.zip)


#### <a name="2"></a> 

**Question:** 


**Answer:** 

**Response:** 

Here is a slightly cleaned up version of Yongyu Deng's code that I added to
[The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2019.0.144.4](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.4):

<pre class="code">
</pre>


#### <a name="3"></a> 
