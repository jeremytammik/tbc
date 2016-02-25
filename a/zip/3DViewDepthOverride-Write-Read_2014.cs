private ICollection<ElementId> Integral(XYZ min1, XYZ max1, XYZ min2, XYZ max2)
		{
			//If the view is not parallel to X or Y axis in Global Coordinates, the selecting process is done using an integral approach
			//each segment is subdivided into a high number of subdivisions (200) and smaller bounding boxes are used to filter the objects
			//that's the reason why with a non-parallel view the command is slower
			//This is the list of ElementId created and immediately cleared
			ICollection<ElementId> ids = new FilteredElementCollector(this.ActiveUIDocument.Document)
				.WhereElementIsNotElementType()
				.ToElementIds();
			ids.Clear();
			List<XYZ> startpoints = new List<XYZ>();
			startpoints.Add(min1);
			startpoints.Add(max1);
			List<XYZ> endpoints = new List<XYZ>();
			endpoints.Add(min2);
			endpoints.Add(max2);
			foreach (Document doc in this.Application.Documents)
			{
				int subdivisions = 200;
				//Here determines the minimum and the maximum point for the segment based on the four vertices that have been passed from the other function
				XYZ a = new XYZ(
					startpoints.Min(p=> p.X),
					startpoints.Min(p=> p.Y),
					startpoints.Min(p=> p.Z));
				XYZ a1 = a;
				XYZ b = new XYZ(
					startpoints.Max(p=> p.X),
					startpoints.Max(p=> p.Y),
					startpoints.Max(p=> p.Z));
				XYZ c = new XYZ(
					endpoints.Min(p=> p.X),
					endpoints.Min(p=> p.Y),
					endpoints.Min(p=> p.Z));
				XYZ d = new XYZ(
					endpoints.Max(p=> p.X),
					endpoints.Max(p=> p.Y),
					endpoints.Max(p=> p.Z));
				XYZ increment = (d - b) / subdivisions;
				startpoints.Clear();
				startpoints.Add(a1);
				startpoints.Add(d);
				for (int i = 0; i < subdivisions; i++)
				{
					//This is tricky: sometimes if the view is perfectly parallel something about the vertices goes wrong (I guess there's a small tolerance)
					//Anyway to reduce the amount of calculations if the coordinates are the same at the fifth decimal digit I assumed the coordinates to be equal
					//I know it isn't perfect but it worked for me
					if (Math.Round(a.X, 5) == Math.Round(b.X, 5) || Math.Round(a.Y, 5) == Math.Round(b.Y, 5))
					{
						i = subdivisions + 1;
						a = new XYZ(
							startpoints.Min(p=> p.X),
							startpoints.Min(p=> p.Y),
							startpoints.Min(p=> p.Z));
						b = new XYZ(
							startpoints.Max(p=> p.X),
							startpoints.Max(p=> p.Y),
							startpoints.Max(p=> p.Z));
					}
					Outline ol = new Outline(a, b);
					if (ol.IsEmpty == false)
					{
						//Here comes the filtering part where I tried to avoid all kinds of objects that are not useful for this task because they can't be overridden
						//such as Element Types or Sketches of Floors/Roofs/ ceilings and so on
						BoundingBoxIntersectsFilter BBIIF = new BoundingBoxIntersectsFilter(ol);
						IEnumerable<Element> elems = new FilteredElementCollector(doc)
							.WherePasses(BBIIF)
							.WhereElementIsNotElementType()
							.WhereElementIsViewIndependent()
							.Cast<Element>()
							.Where(q => q.Category != null && q.Category.HasMaterialQuantities);
						if (elems.Count<Element>() > 0)
						{
							foreach (Element e in elems)
							{
								ids.Add(e.Id);
							}
						}
					}
					else
					{
						a = a + increment;
						b = b + increment;
					}
					a = a + increment;
					b = b + increment;
				}
			}
			return ids;
		}
		private bool ViewDepthOverride3D(View3D view)
		{
			UIDocument uidoc = this.ActiveUIDocument;
			Document doc = uidoc.Document;
			//Gets the bounding box associated with the view
			BoundingBoxXYZ box = view.GetSectionBox();
			Options opt = new Options();
			XYZ min = view.CropBox.Min;
			XYZ max = view.CropBox.Max;
			Transform Ct = view.CropBox.Transform;
			Transform Inverse = Ct.Inverse;
			min = Ct.OfPoint(min);
			max = Ct.OfPoint(max);
			List<XYZ> points = new List<XYZ>();
			IEnumerable<Element> elems = new FilteredElementCollector(doc)
				.WhereElementIsNotElementType()
				.WhereElementIsViewIndependent()
				.Cast<Element>()
				.Where(q => q.Category != null && q.Category.HasMaterialQuantities && q.DesignOption == doc.ActiveView.DesignOption);
			if (elems.Count<Element>() > 0)
			{
				foreach (Element e in elems)
				{
					GeometryElement geomelem = e.get_Geometry(opt);
					points.Add(geomelem.GetBoundingBox().Min);
					points.Add(geomelem.GetBoundingBox().Max);

				}
			}
			points.Add(max);
			points.Add(min);
			XYZ a = new XYZ(
				points.Max(pt => pt.X),
				points.Max(pt => pt.Y),
				points.Max(pt => pt.Z));
			XYZ b = new XYZ(
				points.Min(pt => pt.X),
				points.Min(pt => pt.Y),
				points.Min(pt => pt.Z));
			XYZ c = Inverse.OfPoint(b);
			XYZ d = Inverse.OfPoint(a);
			XYZ Vmin = Inverse.OfPoint(min);
			XYZ Vmax = Inverse.OfPoint(max);
			points.Clear();
			points.Add(c);
			points.Add(d);
			points.Add(Vmin);
			points.Add(Vmax);
			d = new XYZ(
				points.Max(pt => pt.X),
				points.Max(pt => pt.Y),
				points.Max(pt => pt.Z));
			c = new XYZ(
				points.Min(pt => pt.X),
				points.Min(pt => pt.Y),
				points.Min(pt => pt.Z));
			XYZ Va = new XYZ(c.X, c.Y, d.Z);
			XYZ Vb = new XYZ(c.X, d.Y, d.Z);
			XYZ Vc = new XYZ(d.X, d.Y, d.Z);
			XYZ Vd = new XYZ(d.X, c.Y, d.Z);
			XYZ Ve = new XYZ(c.X, c.Y, c.Z);
			XYZ Vf = new XYZ(c.X, d.Y, c.Z);
			XYZ Vg = new XYZ(d.X, d.Y, c.Z);
			XYZ Vh = new XYZ(d.X, c.Y, c.Z);
			Va = Ct.OfPoint(Va);
			Vb = Ct.OfPoint(Vb);
			Vc = Ct.OfPoint(Vc);
			Vd = Ct.OfPoint(Vd);
			Ve = Ct.OfPoint(Ve);
			Vf = Ct.OfPoint(Vf);
			Vg = Ct.OfPoint(Vg);
			Vh = Ct.OfPoint(Vh);
			points.Clear();
			points.Add(Va);
			points.Add(Vb);
			points.Add(Vc);
			points.Add(Vd);
			points.Add(Ve);
			points.Add(Vf);
			points.Add(Vg);
			points.Add(Vh);
			Va = new XYZ(Va.X, Va.Y, points.Min(pt => pt.Z));
			Vb = new XYZ(Vb.X, Vb.Y, points.Max(pt => pt.Z));
			Vc = new XYZ(Vc.X, Vc.Y, points.Max(pt => pt.Z));
			Vd = new XYZ(Vd.X, Vd.Y, points.Min(pt => pt.Z));
			Ve = new XYZ(Ve.X, Ve.Y, points.Min(pt => pt.Z));
			Vf = new XYZ(Vf.X, Vf.Y, points.Max(pt => pt.Z));
			Vg = new XYZ(Vg.X, Vg.Y, points.Max(pt => pt.Z));
			Vh = new XYZ(Vh.X, Vh.Y, points.Min(pt => pt.Z));
			XYZ V1 = Ve + (Va - Ve) * 2 / 3;
			XYZ V2 = Vf + (Vb - Vf) * 2 / 3;
			XYZ V3 = Vg + (Vc - Vg) * 2 / 3;
			XYZ V4 = Vh + (Vd - Vh) * 2 / 3;
			XYZ V5 = Ve + (Va - Ve) / 3;
			XYZ V6 = Vf + (Vb - Vf) / 3;
			XYZ V7 = Vg + (Vc - Vg) / 3;
			XYZ V8 = Vh + (Vd - Vh) / 3;
			ICollection<ElementId> ids0 = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Levels).ToElementIds();
			ICollection<ElementId> ids1 = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Levels).ToElementIds();
			ICollection<ElementId> ids2 = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Levels).ToElementIds();
			ids0.Clear();
			ids1.Clear();
			ids2.Clear();
			ids0 = Integral(V1, Vb, V8, Vc);
			ids1 = Integral(Ve, V2, Vh, V4);
			ids2 = Integral(Ve, V6, Vh, V7);
			if (ids0.Count == 0)
			{
				TaskDialog.Show("View Depth Override - " + view.Name, "Something went wrong in the closer segment.\n\nPlease adjust the view depth to include some objects.");
				ElementId e = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).FirstElement().Id;
				ids0.Add(e);
				ids1.Add(e);
				ids2.Add(e);
			}
			else
			{
				if (ids2.Count == 0)
				{
					TaskDialog.Show("View Depth Override - " + view.Name, "Something went wrong in the farther segment to be overridden in Grey 192.\n\nPlease check that the view depth in the current view is just enough to include the objects you need.");
					ElementId e = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).FirstElement().Id;
					ids2.Add(e);
					ids1.Add(e);
				}
			}
			Color col1= new Color((byte)128,(byte)128,(byte)128);
			Color col2= new Color((byte)192,(byte)192,(byte)192);
			try
			{
				using (Transaction t = new Transaction(doc, "View Depth Override - " + view.Name))
				{
					t.Start();
					OverrideGraphicSettings og0= new OverrideGraphicSettings();
					OverrideGraphicSettings og1= new OverrideGraphicSettings();
					og1.SetProjectionLineColor(col1);
					og1.SetProjectionFillColor(col1);
					OverrideGraphicSettings og2= new OverrideGraphicSettings();
					og2.SetProjectionLineColor(col2);
					og2.SetProjectionFillColor(col2);
					while (ids0.Count > 0)
					{
						//Stores the colour for the foreground
						foreach (ElementId e in ids0)
						{
							og0=new OverrideGraphicSettings(view.GetElementOverrides(e));
						}
						if (ids1.Count != 0)
						{
							//Override the middle segment
							foreach (ElementId e in ids1)
							{
								view.SetElementOverrides(e, og1);
							}
						}
						else
						{
							//Just a precaution, not sure it is really necessary
							TaskDialog.Show("View Depth Override - "+view.Name, "Something went wrong in the middle segment to be overridden in Grey 128.\n\nPlease check that the view depth in the current view is just enough to include the objects you need.");
							break;
						}
						if (ids2.Count != 0)
						{
							foreach (ElementId e in ids2)
							{
								view.SetElementOverrides(e, og2);
							}
						}
						else
						{
							//Override the background segment
							TaskDialog.Show("View Depth Override - "+view.Name, "Something went wrong in the farther segment to be overridden in Grey 192.\n\nPlease check that the view depth in the current view is just enough to include the objects you need.");
							break;
						}
						//Resets the foreground colour in case of objects overlapping foreground and middle segment
						foreach (ElementId e in ids0)
						{
							view.SetElementOverrides(e, og0);
						}
						break;
					}
					doc.Regenerate();
					t.Commit();
				}
			}
			catch (Exception)
			{
				throw;
			}
			return true;
		}
		public void ViewListWrite()
		{
			UIDocument uidoc = this.ActiveUIDocument;
			Document doc = uidoc.Document;
			//Filters for the first perspective view in the model
			IEnumerable<View3D> views = new FilteredElementCollector(doc)
				.OfCategory(BuiltInCategory.OST_Views)
				.WhereElementIsNotElementType()
				.OfClass(typeof(View3D))
				.Cast<View3D>()
				.Where(x => !x.IsTemplate && x.IsPerspective);
			string temp = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			string path = String.Concat(temp, "\\" + doc.Title.Remove(doc.Title.Length - 4) + "-3DViews.txt");
			string output = "";
			using (StreamWriter sw = new StreamWriter(path, false))
			{
				foreach (View3D v in views)
				{
					output = v.Name;
					sw.WriteLine(output);
				}
			}
		}
		public void ViewListRead()
		{
			UIDocument uidoc = this.ActiveUIDocument;
			Document doc = uidoc.Document;
			//Filters for the first perspective view in the model
			IEnumerable<View3D> views = new FilteredElementCollector(doc)
				.OfCategory(BuiltInCategory.OST_Views)
				.WhereElementIsNotElementType()
				.OfClass(typeof(View3D))
				.Cast<View3D>()
				.Where(x => !x.IsTemplate && x.IsPerspective);
			string temp = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			string path = String.Concat(temp, "\\" + doc.Title.Remove(doc.Title.Length - 4) + "-3DViews.txt");
			string output = "";
			string viewN = "";
			int i = 0;
			output = "";
			StreamReader sr = new StreamReader(path);
			using (sr)
			{
				while (!sr.EndOfStream)
				{
					viewN = sr.ReadLine();
					View3D v = views.Where(vi => vi.Name == viewN).First();
					ViewDepthOverride3D(v);
					i += 1;
					output += v.Name + "\n";
				}
			}
			TaskDialog.Show("View Depth Override", "The command has been performed on " + i + " view(s):" + output);
		}