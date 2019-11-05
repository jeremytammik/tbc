<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- create material
  all material and asset properties are accessible except one -- https://autodesk.slack.com/archives/C0SR6NAP8/p1572546797032200
  https://stackoverflow.com/questions/58414284/revit-api-material-with-texture-from-filepath
  https://forums.autodesk.com/t5/revit-api-forum/create-new-material-and-add-new-physical-and-thermal-assets-with/m-p/7311468
  15089722 [Revit Material Library] implemented CreateMaterialAsset sample add-in, reanswered case
  1735_editscope_texture.md
  https://thebuildingcoder.typepad.com/blog/2016/10/list-material-asset-texture-and-forge-webinar-recordings.html#comment-4442686784
  14254618 [Changing material texture path with editScope] reanswered
  https://forums.autodesk.com/t5/revit-api-forum/changing-material-texture-path-with-editscope/m-p/8017578
  15041727 [Unable to get correct texture path from Revit API]
  14887363 [Obtain Uv-coordinates from texture]
  14348365 [Render Material Texture Image]
  https://forums.autodesk.com/t5/revit-api-forum/render-material-texture-image/m-p/8080636
  https://thebuildingcoder.typepad.com/blog/2016/10/list-material-asset-texture-and-forge-webinar-recordings.html#comment-4442686784
    [Q] Great tip, but how can we create the fullpath to the unifiedbitmap_Bitmap texture. I've tried using Application:: GetLibraryPaths but the material library is not listed in the dictionnary :(
    [A] If all else fails, you can try to use the operating system functionality to search globally for the given filename. You should cache the folders in which you find it. If the system is sensibly set up, you will only need to search for it globally once, or a very few times, since there should not be many different locations in which these files are stored.
    [R] Thank you for this tip, especially for the cache recommandation. Works like a charm. :)
  https://forums.autodesk.com/t5/revit-api-forum/create-new-material-and-add-new-physical-and-thermal-assets-with/m-p/7311468
  [Changing material texture path with editScope](https://forums.autodesk.com/t5/revit-api-forum/changing-material-texture-path-with-editscope/m-p/8017578)
  [Render Material Texture Image](https://forums.autodesk.com/t5/revit-api-forum/render-material-texture-image/m-p/8080636)

  [setting material texture path in `EditScope`](https://thebuildingcoder.typepad.com/blog/2019/04/set-material-texture-path-in-editscope.html)
  
- material and asset access
Liz Fortune Oct 31st at 7:33 PM
@conoves @t_arunb Balaji and I have been able to access each and every parameter of the material, appearance asset, thermal asset, and physical asset EXCEPT for one....one lonely parameter...I was wondering if you knew how to connect to the keywords parameter which lives on the Identity tab of the Material UI in Revit.  We can connect to the keywords for all of the assets above but would like to also connect to the keywords that live on the material itself.  Any help would be appreciated

9 replies

Scott Conover  5 days ago
Looks to me that Material.getKeywords() is not exposed (it is not a BuiltInParameter either).  @zhangg can you confirm?
:eyes:
1



Guofeng Zhang  4 days ago
Yes, Keywords are not exposed to API until now.


Jeremy Tammik  4 days ago
@t_fortl @t_arunb thank you very much for confirming that you can access all the material and asset properties. can you possibly share the code that shows all the different paths you needed to take? many external add-in developers keep running into problems accessing this data, so a generic sample demonstrating how to access al  variations would be extremely helpful. thank you!
:+1::skin-tone-2:
1



Balaji Arunachalam  4 days ago
Thanks for confirming @zhangg. Can you please confirm if there is any parameter to get Environment and Render Settings value too? Their values are common for all the Assets, kindly correct me if I am wrong. (edited) 

/a/doc/revit/tbc/git/a/img/assets_environment.png

/a/doc/revit/tbc/git/a/img/assets_render_settings.png

Balaji Arunachalam  4 days ago
@tammikj We have used the code snippets you have shared on your block, however now @t_fortl is in the process of recreating materials with the values we got. Hope we can share you that once we are successful in recreating it.

Liz Fortune  3 days ago

/a/doc/revit/tbc/git/a/zip/material_asset_properties.txt

Balaji @t_arunb  has been heads down working with the appearance assets but the code provided will get you everything for the thermal and structural assets and almost everything for the appearance asset.  The pdf on your blog was crucial in figuring out how to touch all the aspects of the appearance asset!  I have also attached a Snagit image I put together that will show mappings for the appearance asset (the google sheet in this image was an output of my code into a csv).  Hope this helps.  :slightly_smiling_face: (edited) 

/a/doc/revit/tbc/git/a/img/assets_air_appearance_properties.png

Guofeng Zhang  22 hours ago
@t_arunb Nope. As you said, they are common for all assets, so they are think as property or parameters, but UI config and saved into MaterialUIConfig.xml.



twitter:

in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Material, Physical and Thermal Assets




####<a name="2"></a> 

**Question:** 


**Answer:**



<pre class="code">
</pre>


**Response:** 

Many thanks to Eason for confirming this procedure!


####<a name="3"></a> 

<center>
<img src="img/" alt="" width="100"> <!---->
</center>


####<a name="4"></a> 


