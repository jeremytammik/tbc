<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Revit Batch Processor project
  Jan Christel <jan.r.christel@gmail.com>
  I’ve been following your website for a number of years by know and would like to say thank you for your posts and insights. They are a really great help when getting started with the Revit API.
  For the last 3 years I’ve been developing code using the Revit API in context of the Revit Batch Processor project which you are familiar with. The library has now grown to reasonable size and might be useful for others too. It focusses mainly on reporting / modifying element data and families. It show cases how to start a number of batch processor sessions concurrently using either .bat or power shell to process a large number of files.
  A link to the GitHub repo: https://github.com/jchristel/SampleCodeRevitBatchProcessor and the pypi package: https://pypi.org/project/DuHast/

- https://neal.fun/internet-artifacts/

- shapely dynamo
  Durmuş Cesur (Bayryam)
  Hi Jeremy,  I read your article about "Shapely Geometry" in the last weeks, I was very happy to see it because in April I released a package called "Shapely" for Dynamo, so that users can easily work with "Shapely Geometries". I am a BIM Manager, the Shapely package plays an incredible role in our daily work and in the plug-in of the office. Here you can find the contents of the package. Github : https://github.com/DurmusCesur/Shapely.git Linkednl : https://www.linkedin.com/pulse/shapely-dynamo-durmu%25C5%259F-cesur-bayryam-/?trackingId=9aYP9eQpR8%2Be%2B7CCRqldtw%3D%3D  I care a lot about this package because apart from manipulating geometries, it is now integrated with Machine Learning. In the following days, I will release the "1.1.1.1" version of the package and users will now be able to use their scenarios with Machine Learning training models via Shapely, and as a Dynamo user for many years, I am very excited. I just published an article about the Shapely-Machine Learning relationship on linkednl. You can access it here: Linkednl : https://www.linkedin.com/pulse/machine-learning-dynamo-durmu%25C5%259F-cesur-bayryam--m9xoe/?trackingId=9aYP9eQpR8%2Be%2B7CCRqldtw%3D%3D  What are you thinking? I would really like to chat with you about this.  Best regards, Durmus
  Jeremy Tammik (Sir)  4:04 AM
  Dear Durmuş, thank you for letting me know. I will add a note of that to the blog as well, if you like. Cheers, Jeremy.
  Durmuş Cesur(Bayryam)  6:02 AM
  Hi Jeremy,  That would be great, thank you very much.  Best Durmus
  Durmuş Cesur(Bayryam)  9:25 AM
  Hi Jeremy,  Whatever you need for that, you can tell me. I can do it for you

twitter:

the @AutodeskAPS @AutodeskRevit #RevitAPI #BIM @DynamoBIM @AutodeskAPS

&ndash; ...

linkedin:


#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Batch Processor Project and Dynamo Shapely


####<a name="2"></a> Revit Batch Processor Project

Revit Batch Processor project
Jan Christel <jan.r.christel@gmail.com>
I’ve been following your website for a number of years by know and would like to say thank you for your posts and insights. They are a really great help when getting started with the Revit API.
For the last 3 years I’ve been developing code using the Revit API in context of the Revit Batch Processor project which you are familiar with. The library has now grown to reasonable size and might be useful for others too. It focusses mainly on reporting / modifying element data and families. It show cases how to start a number of batch processor sessions concurrently using either .bat or power shell to process a large number of files.
A link to the GitHub repo: https://github.com/jchristel/SampleCodeRevitBatchProcessor and the pypi package: https://pypi.org/project/DuHast/

####<a name="3"></a> Shapely Dynamo

We recently discussed
The [Shapely Python 2D geometry library](https://thebuildingcoder.typepad.com/blog/2023/09/element-diff-compare-shapely-and-rdbe.html#4_ and
how it can be used
to [find and fix a hole](https://thebuildingcoder.typepad.com/blog/2023/09/element-diff-compare-shapely-and-rdbe.html#4.1)



Durmuş Cesur (Bayryam) of [BPA ©Architecture](https://bpa.archi/) announces that
a new dynamo package, the Shapely Python library is now available in Dynamo. Enjoy the polygons !


shapely dynamo
Durmuş Cesur (Bayryam)
Hi Jeremy,  I read your article about "Shapely Geometry" in the last weeks, I was very happy to see it because in April I released a package called "Shapely" for Dynamo, so that users can easily work with "Shapely Geometries". I am a BIM Manager, the Shapely package plays an incredible role in our daily work and in the plug-in of the office. Here you can find the contents of the package. Github : https://github.com/DurmusCesur/Shapely.git Linkednl : https://www.linkedin.com/pulse/shapely-dynamo-durmu%25C5%259F-cesur-bayryam-/?trackingId=9aYP9eQpR8%2Be%2B7CCRqldtw%3D%3D  I care a lot about this package because apart from manipulating geometries, it is now integrated with Machine Learning. In the following days, I will release the "1.1.1.1" version of the package and users will now be able to use their scenarios with Machine Learning training models via Shapely, and as a Dynamo user for many years, I am very excited. I just published an article about the Shapely-Machine Learning relationship on linkednl. You can access it here: Linkednl : https://www.linkedin.com/pulse/machine-learning-dynamo-durmu%25C5%259F-cesur-bayryam--m9xoe/?trackingId=9aYP9eQpR8%2Be%2B7CCRqldtw%3D%3D  What are you thinking? I would really like to chat with you about this.  Best regards, Durmus
Jeremy Tammik (Sir)  4:04 AM
Dear Durmuş, thank you for letting me know. I will add a note of that to the blog as well, if you like. Cheers, Jeremy.
Durmuş Cesur(Bayryam)  6:02 AM
Hi Jeremy,  That would be great, thank you very much.  Best Durmus
Durmuş Cesur(Bayryam)  9:25 AM
Hi Jeremy,  Whatever you need for that, you can tell me. I can do it for you

shapely_dynamo.png


####<a name="4"></a> Internet Artifacts

- https://neal.fun/internet-artifacts/

<pre class="prettyprint">

</pre>

