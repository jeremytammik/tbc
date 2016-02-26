<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- blog:
  - use github as a blog base? via wiki?
  - open source blog platform?
  - keep in parallel with typepad tbc?
  - implement local url base for all links
  - clear out comments, email addresses and confidential stuff
  - put all blog source code on github

- blog on github
  Kean Walmsley has been playing around with [moving Threough the Interface from Typepad to Wordpress]().
  I looked at various alternative options as well.
  http://blog.teamtreehouse.com/using-github-pages-to-host-your-website
  http://jekyllbootstrap.com
  https://help.github.com/articles/using-jekyll-as-a-static-site-generator-with-github-pages/
  [Jekyll executive overview](https://github.com/blog/272-github-pages)
  a Jekyll blog sample [source](https://github.com/mojombo/mojombo.github.io) and [result](http://tom.preston-werner.com)
  a critical view of [blogging on GitHub](http://www.codeproject.com/Articles/809846/Blogging-on-GitHub)
  how and why to move away from Wordpress to [create a beautiful and minimal blog using Jekyll, Github Pages, and poole](http://joshualande.com/jekyll-github-pages-poole)
  I finally ended up doing it the same way as I have handled [hosting The 3D Web Coder source HTML and index on GitHub Pages](http://the3dwebcoder.typepad.com/blog/2015/03/hosting-a-node-server-on-heroku-pages-and-3d-web.html#2)

#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #rendering
#3dweb #3dviewAPI #html5 #threejs #webgl #3d #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restAPI
#mongoosejs #mongodb #nodejs
#rtceur
#xaml
#3dweb #a360 #3dwebaccel #webgl @adskForge
@AutodeskReCap @Adsk3dsMax
#revitAPI #bim #aec #3dwebcoder #adsk #adskdevnetwrk @jimquanci @keanw
#au2015 #rtceur
#eraofconnection
#RMS @researchdigisus
@adskForge #3dwebaccel
#a360

Revit API, Jeremy Tammik, akn_include

 #revitAPI #3dwebcoder @AutodeskRevit #bim #aec #adsk #adskdevnetwrk

&ndash;
...

-->

### tbc &ndash; The Building Coder Source on GitHub

Developers have frequently lamented bad search functionality on Typepad-hosteed blogs.

Furthermore, it is always handy to have an option of caonsulting an offline local copy of any kind of important documentation.

I took these aspects into account right from the start when setting out
on [The 3D Web Coder](http://the3dwebcoder.typepad.com),
as I explained in the discussion
on [hosting its source HTML and index on GitHub Pages](http://the3dwebcoder.typepad.com/blog/2015/03/hosting-a-node-server-on-heroku-pages-and-3d-web.html#2).



hi kean,

i completed my github move.

could you please take a look and tell me what you think?

thank you!

here are the entry points:

https://github.com/jeremytammik/tbc

https://jeremytammik.github.io/tbc/a

http://thebuildingcoder.typepad.com (you know thin one  :-)

thank you!

in case you winder, the reason for the additional 'a' subfolder is that github sets a limit of 1000 to the number of files it lists in a single folder in the UI (example).

without the 'a', the README.md is hidden by the blog posts.

cheers

jeremy




all things considered, my current tendency is to stay put on typepad.

and add the github version for safer backup, providing a version that users can clone for local searching and offline access.

if you point out more of the typepad flaws that really hurt, i'll definitely reconsider!



It’s still a work in progress. Not sure yet whether I’ll flip the switch, but at the very least I’ve learned a lot about the WP system.

Yes, you did mention the idea of moving to GitHub. I won’t be doing that, but it’s an interesting idea.


i am looking at putting tbc onto github, as i already have done with the 3d web coder.

i did that with zero tools or utilities, just straight github pages to hold an index file pointing to the contents:

https://github.com/jeremytammik/3dwc

https://jeremytammik.github.io/3dwc

http://the3dwebcoder.typepad.com

were you aware of that?

that layout is not a blog, of course, but a very handy public search and replication tool.

for a real blog, here is an article that might be of interest to you before cmmitting too deeply to wordpress:

http://joshualande.com/jekyll-github-pages-poole/

i would love to hear your views on that.

thank you!





I have been

I hit a couple of limits built into GitHub and had to implement workarounds to handle some of them:

The two main subfolders a re `img` and `zip` containing images and attachments, respectively, 225 and 410 MB in total size.

Luckily, all my images and attachments are smaller than 100 MB, or GitHub would refuse to host them at all.

As it is, I just got warning messages on two or three files that exceeded a threshold of 30 MB, but were still accepted.

The largest image file is monitor_progress.gif, which is an animated GIF, i.e. a video screen recording, so not a still image at all, weighing in at 77 MB.

The largest attachment is track_changes.mp4, another recording, 63 MB in size.



#### <a name="2"></a>

<center>
<img src="img/.png" alt="" width="600">
</center>





<pre class="code">
</pre>



The updated [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2016.0.126.4](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2016.0.126.4) includes Charles' suggestion as well.
