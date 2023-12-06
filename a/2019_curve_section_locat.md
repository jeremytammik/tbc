<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://github.com/jeremytammik/RevitLookup/releases/tag/2024.0.10
  General
  Introducing a brand new feature: Restore window size! Now, effortlessly you will open RevitLookup with your preferred window dimensions with a simple click
  Improvements
  Add MEPSystem.GetSectionByIndex support by @SergeyNefyodov in #189
  Add MEPSystem.GetSectionByNumber support by @SergeyNefyodov in #189
  Add MEPSection.GetElementIds support by @SergeyNefyodov in #192
  Add MEPSection.GetCoefficient support by @SergeyNefyodov in #192
  Add MEPSection.GetPressureDrop support by @SergeyNefyodov in #192
  Add MEPSection.GetSegmentLength support by @SergeyNefyodov in #192
  Add MEPSection.IsMain support
  Add show System.Object option (named Root hierarchy) by @SergeyNefyodov in #193
  Add generic types support for the help button
  Minor tooltip changes
  Bugs
  Fixed search that worked in the main thread

- Calling Revit command from chromium browser
  https://forums.autodesk.com/t5/revit-api-forum/calling-revit-command-from-chromium-browser/td-p/12413281

- determine element location
  how can the coordinates for a revit fabricationpart be obtained with the revit api
  https://stackoverflow.com/questions/77556660/how-can-the-coordinates-for-a-revit-fabricationpart-be-obtained-with-the-revit-a

- Create a 3D view showing only #Revit wall structural layers
  https://boostyourbim.wordpress.com/2023/12/04/create-a-3d-view-showing-only-revit-wall-structural-layers/
  - Create new 3D isometric view
  - Set parts visibility PartsVisibility.ShowPartsOnly
  - Create parts from all walls
  - For each part, retrieve its built-in parameter DPART_LAYER_INDEX
  - Convert from string to wall compound structure layer index
  - Hide part if its compound structure layer function differs from MaterialFunctionAssignment.Structure

- Dynamo: Curved Sections By Line (Part 1)
  https://youtu.be/Fic5BD-s3A8
  Anna Baranova
  More articles of mine
  https://www.linkedin.com/in/baranovaanna/
  private message
  https://www.linkedin.com/feed/?msgControlName=view_message_button&msgConversationId=2-NDQ2ZDYwYmYtYzY2Yy00MjJlLTgyNTUtM2E4YzA0NTk5MzJhXzAxMg%3D%3D&msgOverlay=true&trk=false

- Researchers quantify the carbon footprint of generating AI images
  Creating a photograph using artificial intelligence is like charging your phone.
  https://www.engadget.com/researchers-quantify-the-carbon-footprint-of-generating-ai-images-173538174.html
  img/ai_image_carbon_footprint.png

- send_data_by_pigeon.jpeg
  https://autodesk.slack.com/archives/C02NW42JP/p1701262599629959
  George Langham
  send_data_by_pigeon.jpeg
  Jean-Philippe Brault
  There is a RFC for that https://datatracker.ietf.org/doc/html/rfc1149
  IETF DatatrackerIETF Datatracker
  RFC 1149: Standard for the transmission of IP datagrams on avian carriers
  This memo describes an experimental method for the encapsulation of IP datagrams in avian carriers. This specification is primarily useful in Metropolitan Area Networks. This is an experimental, not recommended standard.
  1 Apr 1990
  Feisal Ahmad
  Never underestimate the bandwidth of a station wagon full of tapes hurtling down the highway.
  Wikipedia Sneakernet
  Sneakernet, also called sneaker net, is an informal term for the transfer of electronic information by physically moving media such as magnetic tape, floppy disks, optical discs, USB flash drives or external hard drives between computers, rather than transmitting it over a computer network. The term, a tongue-in-cheek play on net(work) as in Internet or Ethernet, refers to walking in sneakers as the transport mechanism. Alternative terms may be floppy net, train net, or pigeon net.
  https://en.wikipedia.org/wiki/Sneakernet
  Ryan Robinson
  I am reminded of this thread from 2012 - https://superuser.com/questions/419070/transatlantic-ping-faster-than-sending-a-pixel-to-the-screen
  (John Carmack rolls in to explain himself as the top answer)
  Super UserSuper User
  Transatlantic ping faster than sending a pixel to the screen?
  John Carmack tweeted,
  I can send an IP packet to Europe faster than I can send a pixel to the screen. How f’d up is that?
  And if this weren’t John Carmack, I’d file it under “the interwebs being

- hope for the future
  Drone Tour of Permaculture Farm
  https://youtu.be/TPxJtKob7Js
  five-minute
  > In this video I narrate a drone tour of our entire 250 acre farm showcasing some of the swale,
  dam, dugout, aquaculture, livestock food forest, cover cropping and other permaculture
  systems we have on our regenerative farm.
  https://www.coenfarm.ca
  > We are literally eating ourselves & our planet to death. Our mission is to provide nutrient-dense food, feed, & permaculture education to regenerate the planet & its people.

twitter:

 @AutodeskRevit  the #RevitAPI @AutodeskAPS  #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

###

####<a name="2"></a>

<pre class="prettyprint">

</pre>


####<a name="3"></a>

**Question:**


<center>
<img src="img/.png" alt="" title="" width="100"/>
</center>


**Answer:**

**Response:**


Thanks to ??? for this explanation.

####<a name="4"></a>




####<a name="4"></a> RevitLookup 2024.0.10

https://github.com/jeremytammik/RevitLookup/releases/tag/2024.0.10

General
Introducing a brand new feature: Restore window size! Now, effortlessly you will open RevitLookup with your preferred window dimensions with a simple click
Improvements
Add MEPSystem.GetSectionByIndex support by @SergeyNefyodov in #189
Add MEPSystem.GetSectionByNumber support by @SergeyNefyodov in #189
Add MEPSection.GetElementIds support by @SergeyNefyodov in #192
Add MEPSection.GetCoefficient support by @SergeyNefyodov in #192
Add MEPSection.GetPressureDrop support by @SergeyNefyodov in #192
Add MEPSection.GetSegmentLength support by @SergeyNefyodov in #192
Add MEPSection.IsMain support
Add show System.Object option (named Root hierarchy) by @SergeyNefyodov in #193
Add generic types support for the help button
Minor tooltip changes
Bugs
Fixed search that worked in the main thread

####<a name="4"></a> Calling Revit Command from Chromium Browser

Calling Revit command from chromium browser

https://forums.autodesk.com/t5/revit-api-forum/calling-revit-command-from-chromium-browser/td-p/12413281

####<a name="4"></a> Determine Element Location

determine element location

how can the coordinates for a revit fabricationpart be obtained with the revit api
https://stackoverflow.com/questions/77556660/how-can-the-coordinates-for-a-revit-fabricationpart-be-obtained-with-the-revit-a

####<a name="4"></a> Create a Structural-Only 3D View

Create a 3D view showing only #Revit wall structural layers

https://boostyourbim.wordpress.com/2023/12/04/create-a-3d-view-showing-only-revit-wall-structural-layers/
- Create new 3D isometric view
- Set parts visibility PartsVisibility.ShowPartsOnly
- Create parts from all walls
- For each part, retrieve its built-in parameter DPART_LAYER_INDEX
- Convert from string to wall compound structure layer index
- Hide part if its compound structure layer function differs from MaterialFunctionAssignment.Structure

####<a name="4"></a> Creating a Curved Section in Dynamo

I have heard several requests for a curved section view, e.g., Alex Vila in 2019:
[Create curved sections!](https://forums.autodesk.com/t5/revit-api-forum/create-curved-sections/m-p/8931972)

Finally, the cavalry comes to the rescue in the shape
of [Anna Baranova](https://www.linkedin.com/in/baranovaanna/), presenting a 22-minute video tutorial
on [Dynamo: Curved Sections By Line (Part 1)](https://youtu.be/Fic5BD-s3A8):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/Fic5BD-s3A8?si=bjREzyZh7uCyrZoZ" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
</center>

Many thanks to Anna for this nice piece of work!

####<a name="4"></a> Carbon Footprint of AI Image Generation

Researchers quantify the carbon footprint of generating AI images

Creating a photograph using artificial intelligence is like charging your phone.
https://www.engadget.com/researchers-quantify-the-carbon-footprint-of-generating-ai-images-173538174.html
img/ai_image_carbon_footprint.png

####<a name="4"></a> Sending Data by Pigeon

Talking about carbon footprint and the cost and efficiency of digital data transmission, there is obviously a point at which transmission of large data can be speeded up by putting it on a storage device and moving that around rather physically than squeezing it through the limited bandwidth of the Internet:

<center>
<img src="img/send_data_by_pigeon.jpg" alt="Send data by pigeon" title="Send data by pigeon" width="600"/> <!-- Pixel Height: 1,218 Pixel Width: 1,080 -->
</center>

- There is even an RFC RFC 1149 for this concept,
the [Standard for the Transmission of IP Datagrams on Avian Carriers](https://datatracker.ietf.org/doc/html/rfc1149).
> This memo describes an experimental method for the encapsulation of IP datagrams in avian carriers.
This specification is primarily useful in Metropolitan Area Networks.
This is an experimental, not recommended standard.
- Never underestimate the bandwidth of a station wagon full of tapes hurtling down the highway,
cf. [Wikipedia on Sneakernet](https://en.wikipedia.org/wiki/Sneakernet).
- Reminds of this thread from 2012
about [transatlantic ping faster than sending a pixel to the screen](https://superuser.com/questions/419070/transatlantic-ping-faster-than-sending-a-pixel-to-the-screen)...

####<a name="4"></a> Permaculture Farm Regenerates Natural Habitat

Hope for the future from a five-minute video [drone tour of permaculture farm](https://youtu.be/TPxJtKob7Js):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/TPxJtKob7Js?si=QoImAfogIIMdU5Sp" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
</center>

> In this video I narrate a drone tour of our entire 250 acre farm showcasing some of the swale,
dam, dugout, aquaculture, livestock food forest, cover cropping and other permaculture
systems we have on our regenerative farm.

Presented by the [Coen Farm](https://www.coenfarm.ca), who say:

> We are literally eating ourselves and our planet to death.
Our mission is to provide nutrient-dense food, feed, and permaculture education to regenerate the planet and its people.

Personally, I was very touched watching and listening to it.

