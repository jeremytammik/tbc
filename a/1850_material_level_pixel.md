<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Creating a material with texture in Autodesk Revit Forge Design Automation
  https://stackoverflow.com/questions/62297851/creating-a-material-with-texture-in-autodesk-revit-forge-design-automation

- cutting off pixels exporting image
  https://forums.autodesk.com/t5/revit-api-forum/export-image-cutting-off-edge-pixels/m-p/9578304
  https://forums.autodesk.com/t5/revit-api-forum/export-image-is-cutting-few-pixels-from-image-corners/m-p/9346019

- change level of element
  https://thebuildingcoder.typepad.com/blog/2014/03/creating-a-sloped-floor.html#comment-4952460602
  Angelo Mastroberardino
  Hello Jeremy,
  one more question. Is it possible to re-set the reference Level of a Floor, once it is created ?
  thank you
  jeremy tammik --> Angelo Mastroberardino
  Dear Angelo,
  In general, the Level property cannot be set after an element has been created.
  It is specified during creation and cannot be modified later.
  Here are some posts on levels, that I searched for and assembled exspecially for you now, both general level handling issues and workarounds to set the level property on certain specific element types:
  Retrieve levels sorted by elevation:
  https://thebuildingcoder.typepad.com/blog/2014/11/webgl-goes-mobile-and-sorted-level-retrieval.html#3
  Family instances lacking or with invalid level property:
  https://thebuildingcoder.typepad.com/blog/2011/01/family-instance-missing-level-property.html
  Changing the level of a floor:
  https://thebuildingcoder.typepad.com/blog/2019/04/set-floor-level-and-use-ipc-for-disentanglement.html#3
  Calculate a level for an element lacking the property:
  https://thebuildingcoder.typepad.com/blog/2019/03/assigning-a-level-to-an-element-missing-it.html
  We have a solution to change the level of an existing room:
  https://thebuildingcoder.typepad.com/blog/2020/03/panel-schedule-slots-and-change-room-level.html

- Physics is cool
  https://www.reddit.com/r/BeAmazed/comments/gxrq8p/physics_is_cool/?utm_source=share&utm_medium=web2x

<blockquote class="reddit-card" data-card-created="1591995928"><a href="https://www.reddit.com/r/BeAmazed/comments/gxrq8p/physics_is_cool/">Physics is cool</a> from <a href="http://www.reddit.com/r/BeAmazed">r/BeAmazed</a></blockquote>
<script async src="//embed.redditmedia.com/widgets/platform.js" charset="UTF-8"></script>

- Forge job openings:
  As you consider your network, here are some ways to think about the qualifications for Forge open roles:
  Experience in a cloud environment such as AWS technologies
  Critical thinkers, problem solvers, story tellers, goal oriented, smart, curiosity, empathy 
  Get started with sharing these roles today:
  20WD39627 – Senior Vendor Manager – San Francisco
  20WD38934 – Localization Software Engineer – Singapore
  20WD37407 – Senior Product Manager, Data – Montreal
  20WD40315 – Senior Data Engineer/Architect – Novi https://autodesk.rolepoint.com/?shorturl=xcqfd#job/ahBzfnJvbGVwb2ludC1wcm9kchALEgNKb2IYgIDItNTSsAoM

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

The question of how to change the colour and material of individual elements has come up repeatedly over time
&ndash; Change element colour in a view
&ndash; Assign new material to an element
&ndash; Replace a material in a wall or floor...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### 

I have been quiet now for a while in shock and grieving about continued racism.

Meanwhile, a bunch of interesting discussions on creating material, modifying element level, cutting off image pixels, enhancing the BipChecker and other things:

- [Creating a material with texture in Revit and Forge](#2)
- [Export image cutting off pixels](#3)
- [Change level of existing element](#4)
- [Physics is cool](#5)
- [Forge job openings](#6)

####<a name="2"></a> Creating a Material with Texture in Revit and Forge

This topic has been very much en vogue lately.
It came up again in the context of Forge in the StackOverflow question
on [creating a material with texture in Autodesk Revit Forge Design Automation](https://stackoverflow.com/questions/62297851/creating-a-material-with-texture-in-autodesk-revit-forge-design-automation),
where [maleficca](https://stackoverflow.com/users/12469767/maleficca) very kindly shares a complete solution for both environments:

**Question:** I'm currently working on some Revit API code which is running in the **Autodesk Forge Design Automation cloud solution**.

Basically, I'm trying to **create a material and attach a texture to it** via the following code:

<pre class="code">
     private void AddTexturePath(AssetProperty asset, string texturePath) {
      Asset connectedAsset = null;

      if (asset.NumberOfConnectedProperties == 0)
       asset.AddConnectedAsset("UnifiedBitmapSchema");
    
      connectedAsset = (Asset) asset.GetConnectedProperty(0);
      AssetPropertyString path = (AssetPropertyString) connectedAsset.FindByName(UnifiedBitmap.UnifiedbitmapBitmap);
    
      if (!path.IsValidValue(texturePath)) {
       File.Create("texture.png");
       texturePath = Path.GetFullPath("texture.png");
      }
    
      path.Value = texturePath;
      
     }
</pre>

This is actually working well, as the value for the texture path:

<pre class="code">
  path.Value = texturePath;
<pre>

Needs to be a reference to an existing file. I do not have this file on the cloud instance of Forge, because the path to the texture name is specified by the user when he sends the request for the Workitem.

The problem is that this sets the texture path for the material as something like this:

<pre class="code">
T:\Aces\Jobs\<workitem_id>\texture.png
<pre>

Which is basically the working folder for the Workitem instance. This path is useless, because a material with texture path like this needs to be manually re-linked in Revit. 

The perfect outcome for me would be if I could somehow map the material texture path to some user-friendly directory like "C:\Textures\texture.png" and it seems that the Forge instance has a "C:\" drive present (being probably a Windows instance of some sorts), but my code runs on low privileges, so it cannot create any kind of directories/files outside the working directory.

Does somebody has any idea how this could be resolved? Any help would be greatly appreciated!

**Answer:** Congratulations on getting to this point.
Would you like to share the code you use to create the material and attach the texture for the Revit API add-in developer community to enjoy, either here or in a new thread in the Revit API discussion forum?
People keep asking for such samples... Thank you!

**Response:** Here is my own answer and code sample:

After a whole day of research I pretty much arrived at a satisfying solution. Just for clarity - I am going to reference to **Autodesk Forge Design Automation API for Revit**, simply as **"Forge"**.

Basically the code provided above is correct. I did not find any possible way to create a file on Forge instance, in a directory different than the Workitem working directory which is:

<pre class="code">
  T:\Aces\Jobs\<workitem_id>\texture.png
<pre>

Interestingly, there is a C:\ drive on the Forge instance, which contains Windows, Revit and .NET Framework installations (as Forge instance is basically some sort of Windows instance with Revit installed). It is possible to enumerate a lot of these directories, but none of the ones I've tried (and I've tried a lot - mostly the most obvious, public access Windows directories like C:\Users\Public, C:\Program Files, etc.) allow for creation of directories or files. This corresponds to what is stated in "Restrictions" area of the Forge documentation:

> Your application is run with low privileges, and will not be able to freely interact with the Windows OS :

> - Write access is typically restricted to the job’s working folder.
> - Registry access is mostly restricted, writing to the registry should be avoided.
> - Any sub-process will also be executed with low privileges.

So after trying to save the "dummy" texture file somewhere on the Forge C:\ drive, I've found another solution - **the texture path for your texture actually does not matter.** 
This is because Revit offers an alternative for re-linking your textures. If you fire up Revit, you can go to File -> Options -> Rendering, and under "Additional render appearance paths" field, you can specify the directories on your local machine, that Revit can use to look for missing textures. With these, you can do the following operations in order to have full control on creating materials on Forge:

1. Send Workitem to Forge, create the materials.
2. Create a dummy texture in working directory, with the correct file name.
3. Attach the dummy texture file to the material.
4. Output the resulting file (.rvt or .rfa, depending on what you're creating on Forge).
5. Place all textures into one folder (or multiple, this doesn't matter that much).
6. Add the directories with the textures to the Additional render apperance paths.
7. Revit will successfully re-link all the textures to new paths.

I hope someone will find this useful!

Additionally, as per Jeremy request, I post a code sample for creating material with texture and modifying different Appearance properties in Revit by using Revit API (in C#):

<pre class="code">
private void SetAppearanceParameters(Document project, Material mat, MaterialData data) {
	using(Transaction setParameters = new Transaction(project, "Set material parameters")) {
		setParameters.Start();

		AppearanceAssetElement genericAsset = new FilteredElementCollector(project)
				.OfClass(typeof(AppearanceAssetElement))
				.ToElements()
				.Cast < AppearanceAssetElement > ().Where(i = >i.Name.Contains("Generic"))
				.FirstOrDefault();

		AppearanceAssetElement newAsset = genericAsset.Duplicate(data.Name);
		mat.AppearanceAssetId = newAsset.Id;

		using(AppearanceAssetEditScope editAsset = new AppearanceAssetEditScope(project)) {
			Asset editableAsset = editAsset.Start(newAsset.Id);
			AssetProperty assetProperty = editableAsset["generic_diffuse"];

			SetColor(editableAsset, data.MaterialAppearance.Color);
			SetGlossiness(editableAsset, data.MaterialAppearance.Gloss);
			SetReflectivity(editableAsset, data.MaterialAppearance.Reflectivity);
			SetTransparency(editableAsset, data.MaterialAppearance.Transparency);

			if (data.MaterialAppearance.Texture != null && data.MaterialAppearance.Texture.Length != 0) 
            AddTexturePath(assetProperty, $@"C:\{data.MaterialIdentity.Manufacturer}\textures\{data.MaterialAppearance.Texture}");

			editAsset.Commit(true);
		}
		setParameters.Commit();
	}
}

private void SetTransparency(Asset editableAsset, int transparency) {
	AssetPropertyDouble genericTransparency = editableAsset["generic_transparency"] as AssetPropertyDouble;
	genericTransparency.Value = Convert.ToDouble(transparency);
}

private void SetReflectivity(Asset editableAsset, int reflectivity) {
	AssetPropertyDouble genericReflectivityZero = (AssetPropertyDouble) editableAsset["generic_reflectivity_at_0deg"];
	genericReflectivityZero.Value = Convert.ToDouble(reflectivity) / 100;

	AssetPropertyDouble genericReflectivityAngle = (AssetPropertyDouble) editableAsset["generic_reflectivity_at_90deg"];
	genericReflectivityAngle.Value = Convert.ToDouble(reflectivity) / 100;
}

private void SetGlossiness(Asset editableAsset, int gloss) {
	AssetPropertyDouble glossProperty = (AssetPropertyDouble) editableAsset["generic_glossiness"];
	glossProperty.Value = Convert.ToDouble(gloss) / 100;
}

private void SetColor(Asset editableAsset, int[] color) {
	AssetPropertyDoubleArray4d genericDiffuseColor = (AssetPropertyDoubleArray4d) editableAsset["generic_diffuse"];
	Color newColor = new Color((byte) color[0], (byte) color[1], (byte) color[2]);
	genericDiffuseColor.SetValueAsColor(newColor);
}

private void AddTexturePath(AssetProperty asset, string texturePath) {
	Asset connectedAsset = null;
	if (asset.NumberOfConnectedProperties == 0) asset.AddConnectedAsset("UnifiedBitmapSchema");

	connectedAsset = (Asset) asset.GetConnectedProperty(0);
	AssetProperty prop = connectedAsset.FindByName(UnifiedBitmap.UnifiedbitmapBitmap);
	AssetPropertyString path = (AssetPropertyString) connectedAsset.FindByName(UnifiedBitmap.UnifiedbitmapBitmap);

	string fileName = Path.GetFileName(texturePath);
	File.Create(fileName);
	texturePath = Path.GetFullPath(fileName);

	path.Value = texturePath;
}
<pre>

Hopefully it will come in handy to someone in the future!

Also, a huge thanks for The Building Coder; it has saved me a lot of hassle in my work with Revit API and enabled to create a lot of cool stuff!

####<a name="3"></a> Export Image Cutting off Pixels

This query just came up again in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Export Image cutting off edge pixels](https://forums.autodesk.com/t5/revit-api-forum/export-image-cutting-off-edge-pixels/m-p/9578304):

**Question:** I'm trying to export family type images from a family, but I'm getting the edge pixels cut off in my exported images.

Here is the original image:

<center>
<img src="img/image_export_cutting_pixels_original.png" alt="Original image" title="Original image" width="223"/> <!-- 223 -->
</center>

This is what comes out:

<center>
<img src="img/image_export_cutting_pixels_exported.png" alt="Exported image" title="Exported image" width="150"/> <!-- 150 -->
</center>

This is what I currently have for my export image options

<pre class="code">
  img = ImageExportOptions()
  img.ZoomType = ZoomFitType.FitToPage
  img.PixelSize = size
  img.ImageResolution = ImageResolution.DPI_150
  img.FitDirection = FitDirectionType.Vertical
  img.ExportRange = ExportRange.SetOfViews
  img.SetViewsAndSheets(viewIds)
  img.HLRandWFViewsFileType = ImageFileType.PNG
  img.FilePath = filepath
  img.ShadowViewsFileType = ImageFileType.PNG
</pre>

A solution was provided by [alexpaduroiu](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/7761409) in a previous conversation
on [export image is cutting few pixels from image corners](https://forums.autodesk.com/t5/revit-api-forum/export-image-is-cutting-few-pixels-from-image-corners/m-p/9346019):

**Question:** I have a small problem regarding `Decoument.ExportImage(ImageExportOptions options)`.

I am trying to export a set of drafting views, but somehow the generated images cuts the views edges.

the Images:

<center>
<img src="img/image_export_cutting_pixels_incomplete.png" alt="Incomplete image" title="Incomplete image" width="400"/> <!-- 500 -->
</center>

This image has the bottom part not visible at all:

<center>
<img src="img/image_export_cutting_pixels_incomplete2.png" alt="Incomplete image" title="Incomplete image" width="279"/> <!-- 279 -->
</center>

The cut is very small but very frustrating because I can't make it a whole or even offset the element to fit the images.

Image Export Options used:

<pre class="code">
ImageExportOptions imgExportOpts = new ImageExportOptions()
{
ZoomType = ZoomFitType.FitToPage,
PixelSize = 500,
FilePath = rbrImagesDirectory + @"\",
FitDirection = FitDirectionType.Vertical,
HLRandWFViewsFileType = ImageFileType.PNG,
ShadowViewsFileType = ImageFileType.PNG,
ImageResolution = ImageResolution.DPI_72,
ShouldCreateWebSite = false,
};

imgExportOpts.ExportRange = ExportRange.SetOfViews
>/pre>

I have tried to modify `FitDirectionType.Horizontal` and this makes it worse than it is now by cutting the bottom portion even more; in that case, for the first image, the bottom part of the bar is not visible at all.

The image doesn't have such a big cut in the edges but it will be nice to have some spaces there or at least to see the parts &nbsp; :-)

Is there any way to zoom out the element or move it in order to be arranged better in image?

**Answer:** Well, I have solved the problem!

The problem was with the drafting view I was trying to export. After creating a group of details in the drafting view, somehow the outline of the view wasn't big enough to include my details. don't know for sure why, but what I have done to resolve the problem was the following:

<pre class="code">
drafting.CropBoxActive = true;
drafting.CropBoxVisible = true;

private static void ExtendViewCrop(View drafting, Group detail)
        {
            BoundingBoxXYZ crop = (drafting!= null ? drafting.CropBox : null);
            if (crop == null || crop.Max == null || crop.Min == null || crop.Transform == null || detail == null)
                return;
            BoundingBoxXYZ detailBox = detail.get_BoundingBox(drafting);
            BoundingBoxXYZ extendedCrop = new BoundingBoxXYZ();
            extendedCrop.Transform = crop.Transform;

            if (detailBox == null || detailBox.Max == null || detailBox.Min == null)
                return;

            extendedCrop.Max = detailBox.Max + (extendedCrop.Transform.BasisX + extendedCrop.Transform.BasisY / 2) * 0.03;
            extendedCrop.Min = detailBox.Min + (-extendedCrop.Transform.BasisX + -extendedCrop.Transform.BasisY / 2) * 0.03;
            drafting.CropBox = extendedCrop;
        }
</pre>

So basically, extending the view crops max and min on their direction with a small value so the detail will fit in my drafting.

I am sure that there are lots of other better options doing this, but for now it did the trick.

Of course I am opened to more solutions &nbsp; :-)

Many thanks to

####<a name="4"></a> Change Level of Existing Element

Angelo Mastroberardino brought up this question in
his [comment](https://thebuildingcoder.typepad.com/blog/2014/03/creating-a-sloped-floor.html#comment-4952460602)
on [creating a sloped floor](https://thebuildingcoder.typepad.com/blog/2014/03/creating-a-sloped-floor.html):

**Question:** Is it possible to re-set the reference Level of a Floor, once it is created ?

**Answer:**  In general, the `Level` property is read-only and thus cannot be set after an element has been created.
It is specified during creation only and cannot be modified later.

Here are some posts on levels, both general level handling issues and workarounds to set the level property on certain specific element types:

- [Retrieve levels sorted by elevation](https://thebuildingcoder.typepad.com/blog/2014/11/webgl-goes-mobile-and-sorted-level-retrieval.html#3)
- [Family instances lacking or with invalid level property](https://thebuildingcoder.typepad.com/blog/2011/01/family-instance-missing-level-property.html)
- [Changing the level of a floor](https://thebuildingcoder.typepad.com/blog/2019/04/set-floor-level-and-use-ipc-for-disentanglement.html#3)
- [Calculate a level for an element lacking the property](https://thebuildingcoder.typepad.com/blog/2019/03/assigning-a-level-to-an-element-missing-it.html)
- A solution to [change the level of an existing room](https://thebuildingcoder.typepad.com/blog/2020/03/panel-schedule-slots-and-change-room-level.html)

####<a name="5"></a> Physics is Cool

A very nice and surprising [physics experiment to try out at home](https://www.reddit.com/r/BeAmazed/comments/gxrq8p/physics_is_cool):

<center>
<blockquote class="reddit-card" data-card-created="1591995928"><a href="https://www.reddit.com/r/BeAmazed/comments/gxrq8p/physics_is_cool/">Physics is cool</a> from <a href="http://www.reddit.com/r/BeAmazed">r/BeAmazed</a></blockquote>
<script async src="//embed.redditmedia.com/widgets/platform.js" charset="UTF-8"></script>
</center>

####<a name="6"></a> Forge Job Openings

Are you a critical thinker, problem solver, story teller, goal oriented, smart, curios, empathic, with experience in a cloud environment such as AWS?

If so, would you like to consider applying for a job in the Forge team?

- [20WD39627 – Senior Vendor Manager – San Francisco](https://rolp.co/SS7ji)
- [20WD38934 &ndash; Localization Software Engineer &ndash; Singapore](https://rolp.co/4Obwi)
- [20WD37407 &ndash; Senior Product Manager, Data &ndash; Montreal](https://rolp.co/Q6cUi)
- [20WD40315 &ndash; Senior Data Engineer/Architect &ndash; Novi ](https://rolp.co/pVKii)

Good luck applying for one of these or the many other opportunities that you can find all over the world in
the [Autodesk career site](https://www.autodesk.com/careers)!

