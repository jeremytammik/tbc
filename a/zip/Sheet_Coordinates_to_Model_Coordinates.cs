		private double DegreesToRadians(double deg)
		{
			return deg/180*Math.PI;
		}
		
		private double RadiansToDegrees(double rad)
		{
			return rad*180/Math.PI;
		}
		
		private double MtoFeet(double m)
		{
			return m/0.3048;
		}
		
		private double MMtoFeet(double mm)
		{
			return mm/304.8;
		}
		
		private double FeetToM(double ft)
		{
			return ft*0.3048;
		}
		
		private double FeetTomm(double ft)
		{
			return ft*304.8;
		}
		
		
		public void QTO_2_PlaceHoldersFromDWFMarkups()
		{
			UIDocument uidoc= this.ActiveUIDocument;
			
			Document doc= uidoc.Document;
			
			View activeView=uidoc.ActiveView;
			
			bool ok=false;
			
			if(activeView is ViewSheet)
			{
				ok=true;
			}
			else
			{
				TaskDialog.Show("QTO","The current view must be a Sheet View with DWF markups");
				return;
			}
			
			ViewSheet vs = activeView as ViewSheet;
			
			Viewport vp =doc.GetElement(vs.GetAllViewports().First()) as Viewport;
			
			View plan=doc.GetElement(vp.ViewId) as View;
			
			int scale= vp.Parameters.Cast<Parameter>().First(x=> x.Id.IntegerValue.Equals((int)BuiltInParameter.VIEW_SCALE)).AsInteger();
			
			IList<Element> dwfMarkups= new FilteredElementCollector(doc)
				.OfClass(typeof(ImportInstance))
				.WhereElementIsNotElementType()
				.Where(x=> x.Name.StartsWith("Markup") && x.OwnerViewId.IntegerValue.Equals(activeView.Id.IntegerValue))
				.ToList();
			
			using (TransactionGroup tg = new TransactionGroup(doc, "DWF markups placeholders"))
			{
				tg.Start();
				
				using (Transaction t = new Transaction(doc,"DWF transfer"))
				{					
					t.Start();
					
					plan.Parameters.Cast<Parameter>().First(x=> x.Id.IntegerValue.Equals((int)BuiltInParameter.VIEWER_CROP_REGION)).Set(1);
					
					XYZ VC=(plan.CropBox.Min+plan.CropBox.Max)/2;
					
					XYZ BC=vp.GetBoxCenter();
					
					t.RollBack();
					
					foreach(Element e in dwfMarkups)
					{
						GeometryElement GeoElem=e.get_Geometry(new Options());
						
						GeometryInstance gi =GeoElem.Cast<GeometryInstance>().First();
						
						GeometryElement gei= gi.GetSymbolGeometry();
						
						IList<GeometryObject> gos= new List<GeometryObject>();
						
						if(gei.Cast<GeometryObject>().Count(x=> x is Arc)>0)
						{
							continue;
						}
						
						foreach(GeometryObject go in gei)
						{
							XYZ med=new XYZ();
							
							if (go is PolyLine)
							{
								PolyLine pl = go as PolyLine;
								
								XYZ min=new XYZ(pl.GetCoordinates().Min(p=> p.X),
								                pl.GetCoordinates().Min(p=> p.Y),
								                pl.GetCoordinates().Min(p=> p.Z));
								
								XYZ max=new XYZ(pl.GetCoordinates().Max(p=> p.X),
								                pl.GetCoordinates().Max(p=> p.Y),
								                pl.GetCoordinates().Max(p=> p.Z));
								
								med= (min+max)/2;
							}
							
							med=med-BC;
							
							// Coordinates in the model
							XYZ a=VC+new XYZ(med.X*scale,med.Y*scale,0);							
						}
					}
					t.Commit();
					t.Start();
					
					foreach(Element e in dwfMarkups)
					{
						GeometryElement GeoElem=e.get_Geometry(new Options());
						
						GeometryInstance gi =GeoElem.Cast<GeometryInstance>().First();
						
						GeometryElement gei= gi.GetSymbolGeometry();
						
						IList<GeometryObject> gos= new List<GeometryObject>();
						
						if(gei.Cast<GeometryObject>().Count(x=> x is Arc)==0)
						{
							continue;
						}
						
						foreach(GeometryObject go in gei)
						{
							if (go is Arc)
							{
								Curve c = go as Curve;
								
								XYZ med=c.Evaluate(0.5,true);
								
								med=med-BC;
								
								XYZ a=VC+new XYZ(med.X*scale,med.Y*scale,0);
								
								doc.Create.NewTextNote(plan,
								                       a,
								                       XYZ.BasisX,
								                       XYZ.BasisY,
								                       MMtoFeet(5),
								                       TextAlignFlags.TEF_ALIGN_CENTER,
								                       activityId);
							}
						}
						
						t.Commit();
					}
				}
				
				tg.Assimilate();
			}
		}