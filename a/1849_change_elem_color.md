<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

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

### Change Element Colour 

This topic keeps coming up.

We discussed it way back

Things have changed since then

Added a new sample command


####<a name="2"></a> 


**Question:** 

**Answer:** 

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 1086 -->
</center>


####<a name="3"></a> 


####<a name="4"></a> 

<pre class="code">
</pre>

16576354 [material assignment]

Salvatore Lauretta, salvatore.lauretta@cemex.com, Cemex Research Group AG

Dear ADN team,

I would like to assign a material from a selected element. The material assigned will come from an excel list. I know already how to create a material from an excel matrix, the part missing is after the creation material, how to assign it to selected elements in revit. Can you help me in this regards please?

Best regards

Salvatore

-----------------------------------------------------------------------
Some of the advice I repeately gave you is being ignored, apparentyly, such as 'research before asking' and ' discussions in public are prioritised'.

If you prefer waiting for an answer instead, it is my pleasure to provide one. Thank you for your patience.

pretty basic

-----------------------------------------------------------------------
Email: Regarding [CaseNo:16576354.] material assignment	5/28/2020 5:58 AM	Outbound

Dear Salvatore,

Thank you for your query.

The Building Coder has repeately addressed the question on controlling an element's colour and material:

https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.24

Furthermore, searching for 'set element color' in the Revit API discussion group returns several solutions, for instance:

How change the color a element? -- https://forums.autodesk.com/t5/revit-api-forum/how-change-the-color-a-element/m-p/5651177
Change color by element id + color palette -- https://forums.autodesk.com/t5/revit-api-forum/change-color-by-element-id-color-palette/m-p/4768209

I do not really have anything to add to these.

I hope this helps.

By the way, as I already suggested to you repeatedly in the past, please also note that the preferred method to submit non-confidential ADN queries or requests on the Revit API like this is via the Revit API discussion forum:

http://forums.autodesk.com/t5/revit-api/bd-p/160

Any thread that you submit there using your email address registered as an ADN member will be recognised as such and automatically escalated to us in the ADN team.

At the same time, you will address a larger audience, more of your peers will see it, be able to chip in and help, and more people will see and profit from the answers we provide.

We therefore prioritise cases from the discussion forum.

Please submit all your non-confidential queries via the Revit API discussion forum.

Also, of course, please always perform at least some minimal research and Internet searching of your own before raising a question at all.

That will save a lot of time in the long run for both you yourself and the rest of the universe.

Thank you for your understanding and cooperation.

Best regards,

-----------------------------------------------------------------------
RE: Regarding [CaseNo:16576354.] material assignment	6/5/2020 9:37 AM	Inbound

Thanks Jeremy for your answer.

Unfortunately my Autodesk account is linked to my company email and my department does not like to public question with that account, I hope in your understanding.

Regarding your link, I would like to ask you if you could send me a reproducible case for changing material in a selected wall and floor. The link sent to me I was not able to merge with my purposes. I hope you will help me in this regards

Best Regards

Salvatore

Salvatore Lauretta
Construction Engineer Specialist
Global R&D and Intellectual Property Management
Office : +41(32)3667886 Mobile: +41 76 3032907
Address: CEMEX Research Group AG. Römerstrasse 13, 2555 Brügg b. Biel
e-Mail: salvatore.lauretta@cemex.com
www.cemex.com

-----------------------------------------------------------------------

Dear Salvatore,

Thank you for your explanation and sorry to hear that you are faced with such serious challenges.

I put together a new sample command for you following the guidelines provided in the links above in The Building Coder samples:

https://github.com/jeremytammik/the_building_coder_samples

It uses the OverrideGraphicSettings class and its SetProjectionLineColor method to change the colour of a selected element in the current view:

https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdChangeElementColor.cs

  [Transaction( TransactionMode.Manual )]
  public class CmdChangeElementColor : IExternalCommand
  {
    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements )
    {
      UIApplication uiapp = commandData.Application;
      UIDocument uidoc = uiapp.ActiveUIDocument;
      Document doc = uidoc.Document;
      View view = doc.ActiveView;
      ElementId id;

      try
      {
        Selection sel = uidoc.Selection;
        Reference r = sel.PickObject(
          ObjectType.Element,
          "Pick element to change its colour" );
        id = r.ElementId;
      }
      catch( Autodesk.Revit.Exceptions.OperationCanceledException )
      {
        return Result.Cancelled;
      }

      Color color = new Color(
        (byte) 200, (byte) 100, (byte) 100 );

      OverrideGraphicSettings ogs = new OverrideGraphicSettings();
      ogs.SetProjectionLineColor( color );

      using( Transaction tx = new Transaction( doc ) )
      {
        tx.Start( "Change Element Color" );
        doc.ActiveView.SetElementOverrides( id, ogs );
        tx.Commit();
      }
      return Result.Succeeded;
    }
  }

Best regards,

-----------------------------------------------------------------------
