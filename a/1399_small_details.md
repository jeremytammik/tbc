<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

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

Revit API, Jeremy Tammik, akn_include

Modelling Small Details #revitAPI #bim #aec #3dwebcoder #adsk @AutodeskRevit #adskdevnetwrk

I am back from the BIM Programming conference and workshops in Madrid and rather flooded, so here is just a quick note on how to you can model small details in Revit, courtesy of Jose Ignacio Montes of Avatar BIM.
As you are perfectly well aware, Revit will not allow you to model things smaller than 1/8th of an inch directly in the project environment.
Jose presents a simple workaround using an imported DWG file...

-->

### Modelling Small Details

I am back from
the [BIM Programming](http://www.bimprogramming.com)
[conference and workshops in Madrid](http://thebuildingcoder.typepad.com/blog/2016/01/bim-programming-madrid-and-spanish-connectivity.html) and
rather flooded with overdue stuff, so here is just a quick note on how to you can model small details in Revit, if you really need to, courtesy
of Jose Ignacio Montes of [Avatar BIM](http://avatarbim.com).

As you are perfectly well aware, Revit will not allow you to model things smaller than 1/8<sup>th</sup> of an inch directly in the project environment, as we have seen by trying to create smaller and smaller line and direct shape elements until an exception is thrown:

- [Think Big in Revit](http://thebuildingcoder.typepad.com/blog/2009/07/think-big-in-revit.html)
- [DirectShape Performance and Minimum Size](http://thebuildingcoder.typepad.com/blog/2014/05/directshape-performance-and-minimum-size.html)


Jose presents a simple workaround using an imported DWG file:

Este es un veierteaguas de un remate de cubierta. Lleva dos tornillos que se repiten en más sitios, por lo que son 'detail items' insertados.

*This shows a gutter cover. It has two little screws that are repeated in other places, so they are inserted as detail items.*

<center>
<img src="img/jim_small_detail_1.png" alt="Revit design with small details" width="661">
</center>

El tornillo está formado por una máscara de líneas invisibles y un DWG importado con todo su detalle.

*The screw is formed by a mask of invisible lines and an imported DWG with all its detail.*

<center>
<img src="img/jim_small_detail_2.png" alt="Masking lines" width="50%">
</center>

<center>
<img src="img/jim_small_detail_3.png" alt="Detailed screw DWG" width="50%">
</center>

La familia de Clestra Metropoline usa el mismo sistema, pero dentro de un perfil de muro cortina. Este es un truco muy útil porque a poca gente se le ocurre meter un DWG dentro de un profile!

*Our Clestra Metropoline family uses the same system, but within a curtain wall profile. This is a very useful trick that few people are aware of, to embed a DWG within a profile!*

Lo mismo puede hacerse con ventanas, puertas o cualquier otro elemento que tenga detalles muy finos.

*You can use the same idea with windows, doors or any other element that includes very fine detail.*

Samples:

- [MODULO_VIERTEAGUAS.rfa](zip/MODULO_VIERTEAGUAS.rfa) &ndash; gutter cover family
- [CLESTRA_METROPOLINE.rvt](zip/CLESTRA_METROPOLINE.rvt) &ndash; Clestra Metropoline curtain wall profile

Many thanks to Jose for sharing this nice approach!
