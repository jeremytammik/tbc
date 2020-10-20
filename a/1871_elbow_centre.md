<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- eliminated deprecated API usage warnings by removing calls to pre-ForgeTypeId unit functionality
  https://github.com/jeremytammik/the_building_coder_samples/compare/2021.0.150.5...2021.0.150.6

- How to calculate the center point of elbow?
  https://forums.autodesk.com/t5/revit-api-forum/how-to-calculate-the-center-point-of-elbow/m-p/9804339
  elbow_arc_centre_point_1.png + 2

- Neo Sheng <wc36170565@gmail.com>
  FireRevit: Using Revit files to identify the room locations of fires and escape routes
  https://forums.autodesk.com/t5/revit-api-forum/firerevit-using-revit-files-to-identify-the-room-locations-of/td-p/9809450
  FireRevit GitHub repository https://github.com/LuhanSheng/Revit_To_Database
  FireRevit: Using Revit Files to Identify the Room Locations of Fires and Escape Routes.
  https://cs.nyu.edu/media/publications/RevitToDatabase.pdf
  1468_hololens_exitpath.md

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

### Calculating Elbow Centre Point

####<a name="2"></a> Revit 2021 DisplayUnitType

Stephen Harrison raised and solved an issue on handling 
[Revit 2021 `DisplayUnitType`](https://forums.autodesk.com/t5/revit-api-forum/revit-2021-displayunittype/m-p/9810626):

**Question:** I am struggling to update a section of my code I have been utilising for a few years now.

In Revit 2021, it is causing a deprecated API usage warning, so I need to update it:

- warning CS0618: `DisplayUnitType` is obsolete:
This enumeration is deprecated in Revit 2021 and may be removed in a future version of Revit.
Please use the `ForgeTypeId` class instead.
Use constant members of the `UnitTypeId` class to replace uses of specific values of this enumeration.

My code snippet is:

<pre class="code">
case StorageType.Double:
double? nullable = t.AsDouble(fp);
if (nullable.HasValue)
{
DisplayUnitType displayUnitType = fp.DisplayUnitType;
value = UnitUtils.ConvertFromInternalUnits(nullable.Value, displayUnitType).ToString();
break;
} 
</pre>

Note: `t` is a `FamilyType` and `fp` is a `FamilyParameter` object.

**Answer:** Embarrassing how simple the solution was:

<pre class="code">
//Pre 2021
                        DisplayUnitType displayUnitType = fp.DisplayUnitType;
                        value = UnitUtils.ConvertFromInternalUnits(nullable.Value, displayUnitType).ToString();

                        //2021

                        ForgeTypeId forgeTypeId = fp.GetUnitTypeId();
                        value = UnitUtils.ConvertFromInternalUnits(nullable.Value, forgeTypeId).ToString();
</pre>

Many thanks to Stephen for sharing this!

####<a name="3"></a> Eliminated TBC Samples Deprecated API Usage

Stephen's question and answer prompted me to take another look at and try to eliminate the deprecated API usage warnings
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples).
After the initial [flat migration to Revit 2021](https://thebuildingcoder.typepad.com/blog/2020/04/2021-migration-add-in-language-and-bim360-login.html#4),
the compilation still generates [162 warnings](zip/tbc_samples_2021_migr_01.txt),
all associated with deprecated methods and enumerations caused by 
the [Units API changes](https://thebuildingcoder.typepad.com/blog/2020/04/whats-new-in-the-revit-2021-api.html#4.1.3).

The resolution was actually quite simple.

I removed some sections of code completely that dealt exclusively with the Revit Unit API functionality, since they ought to be rewritten from scratch for the new unit handling methods. It would make little sense to migrate them step by step. That left a handful of trivial issues to fix.

The new [release 2021.0.150.6](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2021.0.150.6) compiles with zero errors and warnings.

You can see exactly how that was achieved by analusing
the [differences to the previous release](https://github.com/jeremytammik/the_building_coder_samples/compare/2021.0.150.5...2021.0.150.6).

####<a name="4"></a> Calculating the Elbow Centre

- How to calculate the center point of elbow?
  https://forums.autodesk.com/t5/revit-api-forum/how-to-calculate-the-center-point-of-elbow/m-p/9804339
  elbow_arc_centre_point_1.png + 2

**Question:** How to calculate the center point of elbow?

I am trying to get to center point of an elbow. I have check all the data of the elbow from Revit Lookup tool but did not find anything. So is there anyway I can get that information ?

<center>
<img src="img/elbow_arc_centre_point_1.png" alt="Elbow arc centre point" title="Elbow arc centre point" width="100"/> <!-- 1539 -->
</center>

**Answer:** Yes.

This is a pretty simple geometrical exercise.

You have the full information about the start and end point, specifically including location and direction == face normal.

The start and end point directions are not parallel. Therefore, they define a 2D plane. The centre point will normally lie in that plane, so you just have a (simpler) 2D task to solve, not 3D.

In the 2D plane, you can determine the normal vector to the start and end directions. Extend those two normal vectors to define infinite lines and determine their intersection. That is your centre point.

**Response:** Thank you for your answer.
I just found out that actually the elbow already has the center point information under its Geometry information:

<center>
<img src="img/elbow_arc_centre_point_2.png" alt="Snooping elbow arc centre point" title="Snooping elbow arc centre point" width="100"/> <!-- 1539 -->
</center>

So my working code is:

<pre class="code">
  static public XYZ GetCenterofElbow (FamilyInstance selectedDuct)
        {
            XYZ output = null; 
            List<Connector> allConnectors = selectedDuct.MEPModel.ConnectorManager.Connectors.Cast<Connector>().ToList();
            
            Connector connectorA = allConnectors[0]; 
            Connector connectorB = allConnectors[0];

            GeometryElement geometryElement = selectedDuct.get_Geometry(new Options());
            List<GeometryInstance> ginsList = selectedDuct.get_Geometry(new Options()).Where(o => o is GeometryInstance).Cast<GeometryInstance>().ToList();

            foreach (GeometryInstance gins in ginsList)
            {
                foreach (GeometryObject ge in gins.GetInstanceGeometry())
                {
                    try
                    {
                        Arc centerArc = ge as Arc;
                        output = centerArc.Center; 
                    }
                    catch (Exception)
                    {
                       
                    }
                }
            }

            return output;

        }
</pre>

**Answer:** Thank you for letting us know that simple solution!

Oh dear. I wasted some effort, then.

I implemented a geometrical solution for you based on the elbow connectors and their coordinate systems:

- [Connector Origin](https://www.revitapidocs.com/2020/28a9cf5e-9191-f9ce-74c8-622a681201f6.htm)
- [CoordinateSystem property returning a Transform object](https://www.revitapidocs.com/2020/cb6d725d-654a-f6f3-fed0-96cc618a42f1.htm)
- [ew `GetElbowConnectors` and `GetElbowCentre` methods in The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdRectDuctCorners.cs#L240-L319) 

<pre class="code">
    /// <summary>
    /// Return elbow connectors.
    /// Return null if the given element is not a 
    /// family instance with exactly two connectors.
    /// </summary>
    List<Transform> GetElbowConnectors( Element e )
    {
      List<Transform> xs = null;
      FamilyInstance fi = e as FamilyInstance;
      if( null != fi )
      {
        MEPModel m = fi.MEPModel;
        if( null != m )
        {
          ConnectorManager cm = m.ConnectorManager;
          if( null != cm )
          {
            ConnectorSet cs = cm.Connectors;
            if( 2 == cs.Size )
            {
              xs = new List<Transform>( 2 );
              bool first = true;
              foreach( Connector c in cs )
              {
                if( first )
                {
                  xs[0] = c.CoordinateSystem;
                }
                else
                {
                  xs[1] = c.CoordinateSystem;
                }
              }
            }
          }
        }
      }
      return xs;
    }
    
    /// <summary>
    /// Return elbow centre point.
    /// Return null if the start and end points 
    /// and direction vectors are not all coplanar.
    /// </summary>
    XYZ GetElbowCentre( Element e )
    {
      XYZ pc = null;
      List<Transform> xs = GetElbowConnectors( e );
      if( null != xs )
      {
        // Get start and end point and direction

        XYZ ps = xs[ 0 ].Origin;
        XYZ vs = xs[ 0 ].BasisZ;

        XYZ pe = xs[ 1 ].Origin;
        XYZ ve = xs[ 1 ].BasisZ;

        XYZ vd = pe - ps;

        // For a regular elbow, Z vector is normal 
        // of the 2D plane spanned by the coplanar
        // start and end points and direction vectors.

        XYZ vz = vs.CrossProduct( vd );

        if( !vz.IsZeroLength() )
        {
          XYZ vxs = vs.CrossProduct( vz );
          XYZ vxe = ve.CrossProduct( vz );
          pc = Util.LineLineIntersection( 
            ps, vxs, pe, vxe );
        }
      }
      return pc;
    }
</pre>

Would you like to test my code and see whether it returns the same result?

By the way, some elbow families have no arc segment at all.

For example, some can consist of two 45-degree segments, connected by a cylindrical part in between.

In this case, there would be two arcs with different center points, so the connector based approach seems more flexible for different family content.

For example, here is a typical [German chimney exhaust elbow](https://www.ofenseite.com/1020147-pelletofenrohr-90-bogen-mit-kesselanschluss-muffe)

<center>
<img src="img/elbow_arc_centre_point_3.jpg" alt="Chimney 90 degree elbow" title="Chimney 90 degree elbow" width="100"/> <!-- 1539 -->
</center>

In this case, there isn't any arc at all.

The connector-based code should solve the task for that as well.

Please confirm that it works.

The code is untested.


