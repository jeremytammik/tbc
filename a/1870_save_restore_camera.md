<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- restore 3D view camera orientation
  https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html#comment-5106250525

- Ehsan [@eirannejad](https://github.com/eirannejad) Iran-Nejad's [Revit cheatsheets](https://github.com/eirannejad/revit-cheatsheets)
  eirannejad_cheatsheet_keynote_file.png
  > Here is all the Revit cheat sheets I made in the past years to make life easier working with Revit. Want to add yours as well?!
  https://twitter.com/eirannejad/status/1313890807368228864

twitter:

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

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


### Save and Restore Camera


####<a name="2"></a>


https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html#comment-4929308181

Valerii Nozdrenkov

Dear Jeremy,

I have a question about View3D Camera settings. I want to serialize view 3D camera orientation. It works fine if a projection mode is perspective. When I zoom in/out method GetOrientation gives correct values for Perspective camera (position of the camera is changed), but in Orthographic projection mode GetOrientation always returns the same value regardless of zoom in/out (position of the camera is changed), so I can't recreate saved camera orientation in Orthographic mode. There should be another Transformation to apply, but I don't know where to take it from.

I need to change position manually in order to apply changes made by zoom in/out.

I found, there is a method GetZoomCorners of class UIView, this is a bounding box. It's values changed after zoom in/out, but how to move EyePosition according to it.

Any suggestions?

Thanks.

https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html#comment-5106250525

Valerii Nozdrenkov --> Valerii Nozdrenkov

I investigated the problem of saving/restoring current Revit View 3D. For a perspective projection mode, it was very simple, just save camera parameters and then restore them. But for orthographic projection mode, I found that camera parameters are not changed after zooming or panning the model (compare figure 1 and figure 2).

Thumbnail

/a/doc/revit/tbc/git/a/img/vn_3d_camera_orientation_1.png

Figure 1 Camera parameters before zooming and panning

Thumbnail

/a/doc/revit/tbc/git/a/img/vn_3d_camera_orientation_2.png

Figure 2 Camera parameters after zooming and panning

After desperate Googling I found the solution of this task in GitHub, in the 
[RevitView.cs module](https://github.com/teocomi/BCFier/blob/master/Bcfier.Revit/Data/RevitView.cs) of
the [BCFier project](https://github.com/teocomi/BCFier)
by [@mgrzelak](https://github.com/mgrzelak).

The idea is:

Saving:

1. Get corners of the active UI view Corner1 {x1,y1,z1} and Corner2 {x2,y2,z2}

  IList<uiview> views = uidoc.GetOpenUIViews();
  UIView currentView = views.Where(t => t.ViewId == view3D.Id).FirstOrDefault();
  //Corners of the active UI view
  IList<xyz> corners = currentView.GetZoomCorners();
  XYZ corner1 = corners[0];
  XYZ corner2 = corners[1];

2. Calculate center point {0.5(x1+x2),0.5(y1+y2),0.5(z1+z2)}

  double x = (corner1.X + corner2.X) / 2;
  double y = (corner1.Y + corner2.Y) / 2;
  double z = (corner1.Z + corner2.Z) / 2;
  //center of the UI view
  XYZ viewCenter = new XYZ(x, y, z);

3. Calculate diagonal vector

  diagVector = Corner1-Corner2={x1-x2,y1-y2,z1-z2}
  XYZ diagVector = corner1 - corner2;

4. Get up and right vectors

  ViewOrientation3D viewOrientation3D = view3D.GetOrientation();
  XYZ upDirection = viewOrientation3D.UpDirection;
  XYZ rightDirection = forwardDirection.CrossProduct(upDirection);

5. Calculate height=abs(diagVector*upVector);

  double height = Math.Abs(diagVector.DotProduct(upDirection));

6. Find scale = 0.5*height

But, the provided solution doesn’t work correctly if the height > width.

So, we need to take into account both height and width:

  double height = Math.Abs(diagVector.DotProduct(upDirection));
  double width = Math.Abs(diagVector.DotProduct(rightDirection));

Then we have to find the minimal minside = min(height,width)

  double minside = Math.Min(height, width);

6. scale = 0.5*minside

7. Save center point (eyePosition=viewCenter), upDirection, forwardDirection and scale.

Restoring is the same as in provided above link to GitHub project:

1. Get prevously saved eyePosition, upDirection, forwardDirection and scale.
2. Move the camera to preciously saved eyePosition

  var orientation = new ViewOrientation3D(eyePosition, upDirection, forwardDirection);
  view3D.SetOrientation(orientation);

3. Calculate corners of square

Up_Left:

  Corner1 = eyePosition+scale*upVector-scale*rightVector

Down_Right:

  Corner2 = eyePosition-scale*upVector+scale*rightVector

  XYZ Corner1 = position + upDirection* scale - uidoc.ActiveView.RightDirection * scale;
  XYZ Corner2 = position - upDirection* scale + uidoc.ActiveView.RightDirection * scale;

4. ZoomCorners

  ZoomAndCenterRectangle(Corner1, Corner2);
  uidoc.GetOpenUIViews().FirstOrDefault(t => t.ViewId == view3D.Id).ZoomAndCenterRectangle(Corner1, Corner2);

See figure 4 in the reply below.

jeremy tammik --> Valerii Nozdrenkov

Dear Valerii,

Wow! Thank you very much for this impressive solution! I'll work about putting this into a main blog post for enhanced readability and visibility.

Cheers, Jeremy.

https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html#comment-5106461462

Valerii Nozdrenkov --> jeremy tammik

Thanks Jeremy, I just wanted to ask you about it. I'm going to add some extra info to the post today, then you can post it in your blog.

https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html#comment-5106543888

https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html#comment-5106546925

https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html#comment-5107042312

Valerii Nozdrenkov --> Valerii Nozdrenkov

I have added 2 figures:

Thumbnail

Figure 3 - Scale calculation

Thumbnail

Figure 4 - Zooming corners

jeremy tammik --> Valerii Nozdrenkov

Very cool!

Is it ready for posting now, or did you want to add anything more?

Would you like share a small complete sample, so one doesn't have to put together the individual snippets piece by piece?

Thank you!

https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html#comment-5107053457

Valerii Nozdrenkov --> jeremy tammik

Thanks Jeremy,

It's ready!

A complete sample is a good idea, but It will take some time. I let you know when it's ready. Should it be on GitHub?

jeremy tammik --> Valerii Nozdrenkov

Yes, absolutely! GitHub would be great! Thank you! Happy Monday!

https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html#comment-5107690269

Valerii Nozdrenkov --> jeremy tammik

Hi Jeremy,

I have prepared a sample project for described above task Sample project link on GitHub:

https://github.com/Valerii-Nozdrenkov/RevitOrthoCamera

Please, publish it in your blog, if you find it acceptable.

Thanks.

jeremy tammik --> Valerii Nozdrenkov

With great pleasure! Thank you very much indeed for all your research and documentation!

**Question:**

**Answer:** 

**Response:** Thanks

<pre>
</pre>


<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 519 -->
</center>

####<a name="3"></a> 
