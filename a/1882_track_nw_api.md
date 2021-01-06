<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- week of rest until ther twelfth night
  https://en.wikipedia.org/wiki/Twelfth_Night_(holiday)

- filter for detail lines
  Collect All Detail Lines of a Particular SubCategory
  https://forums.autodesk.com/t5/revit-api-forum/collect-all-detail-lines-of-a-particular-subcategory/td-p/9956260
  
- modification tracking
  https://data-shapes.io/2016/12/31/modification-tracking-with-dynamo-and-data-shapes
  Modification Tracking with Dynamo

- 17292711 [Most optimized way of getting a complete set of property categories and properties in a model.]
  
- new
  Node.js Best Practices
  https://github.com/goldbergyoni/nodebestpractices

-  old
  The early history of programming and the C language
  https://arstechnica.com/features/2020/12/a-damn-stupid-thing-to-do-the-origins-of-c/

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
<p style="font-size: 80%; font-style:italic">
<a href=""></a>
</p>
</center>

-->

### Happy New Year, NavisWorks and Modification Tracking

Happy New Year!

As always, there is lots going on in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160),
where Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
is providing tremendous help on all the realy hard questions requiring both Revit API understanding and
in-depth product usage experience.

I played around a bit with the NavisWorks API in the week of rest and the peaceful time until 
the [twelfth night](https://en.wikipedia.org/wiki/Twelfth_Night_(holiday)),
and discovered a very nice Revit project modification tracking tool in Dynamo:

####<a name="2"></a> Filter for Detail Lines Subcategory

One of the threads addressed by Richard is on filtering for detail lines,
to [collect all detail lines of a particular subcategory](https://forums.autodesk.com/t5/revit-api-forum/collect-all-detail-lines-of-a-particular-subcategory/td-p/9956260):

**Question:** From reading The Building Coder's post about Retrieving All Available Line Styles (https://thebuildingcoder.typepad.com/blog/2013/08/retrieving-all-available-line-styles.html) it is my understanding that detail line elements can be collected via FilteredElementCollector once a subcategory is selected? I have a line style subcategory and would like to collect all the detail lines of that style. How do I do that?

For context from the link above (bold added by me for emphasis):
"

While the Revit API does not provide a true 'Line style' element, the line styles are actually subcategories of the Lines category. Therefore, the FilteredElementCollector cannot easily be used for this in a single statement, like in your examples above.

It should be possible to retrieve the line styles without a line instance, though.

Here’s a macro that lists all subcategories of the Lines category:

<pre class="code">
  public void GetListOfLinestyles( Document doc )
  {
    Category c = doc.Settings.Categories.get_Item(
      BuiltInCategory.OST_Lines );
 
    CategoryNameMap subcats = c.SubCategories;
 
    foreach( Category lineStyle in subcats )
    {
      TaskDialog.Show( "Line style", string.Format(
        "Linestyle {0} id {1}", lineStyle.Name,
        lineStyle.Id.ToString() ) );
    }
  }
Note that some line styles like 'Room Boundary' cannot actually be assigned to arbitrary lines in the UI, but this should be good enough to find a usable one.

Once you have a collection of the line style subcategories of interest, you can create a filtered element collector retrieving all ElementType elements belonging to any one of them.

"

**Answer:** For example, changing OST_Lines to OST_LightFixtures will find all the line subcategories of the light fixtures.

(As you type in BuiltInCategory, you should get a list of all the subcatagories you can use.)

See the attached image:

filter_detail_lines_object_styles.png 783

Using the OST_LightFixtures will return "Hidden Lines", "light Source", "test_lightfixturelines", and "test_lightfixturelines2".

**Response:** Thanks for your reply. I don't quite understand your meaning.

I should clarify, I'm looking for the right way to use a Line Style subcategory in a FilteredElementCollector to grab all the detail lines in the project that are of that style. I don't actually use C#, that code is just from the The Building Coder's website, but I have gotten to the point in my code that I have the Line Style I want to filter with and now I would like to create a filtered element collector retrieving all ElementType elements belonging to that Line Style, using the Revit API.

**Answer:** If you aren't using C#, then what are you using?  Python?  Dynamo?  

**Response:** I can translate from anything to anything. Maybe what I'm asking to do isn't possible, I'm not sure.

Actually, I may have found something. I looked up this older forum post: https://forums.autodesk.com/t5/revit-api-forum/filteredelementcollector-gt-get-all-instances-except-... 

<pre class="code">
FilteredElementCollector collector = new FilteredElementCollector(doc);
ElementCategoryFilter fi = new ElementCategoryFilter(BuiltInCategory.OST_TitleBlocks, true);
ICollection<Element> collection 
  = collector.OfClass(typeof(FamilyInstance)).WherePasses(fi)
                .ToElements();
</pre>

I used the ElementCategoryFilter but replaced the built-in category with my category (not built-in) and I removed the .OfClass filter.... although it is collecting 23,000+ lines which doesn't seem right either, so maybe I need to apply another filter 😕

**Answer:** I believe its a 3 step process.  You find all lines, then narrow those down to the Detail Lines, and then narrow those down to get the line style you want.

Add a new line style called "MyNewLineStyle"  (match the caps exactly) and try something like

<pre class="code">
FilteredElementCollector collector = new FilteredElementCollector(doc); 
			ElementCategoryFilter fi = new ElementCategoryFilter(BuiltInCategory.OST_GenericLines, true); 
			ICollection<Element> collection = collector.OfClass(typeof(CurveElement)).WherePasses(fi) .ToElements();
			
			
			TaskDialog.Show("Number of curves", collection.Count.ToString ());
			List<Element> detail_lines = new List<Element>();
			
			
			foreach (Element e in collection)
			{
				if (e as DetailLine != null)
					
				{
					detail_lines.Add (e);
				
				}
			}
		
			
			
			TaskDialog.Show("Number of Detail Lines", detail_lines.Count.ToString ());
    		
    		
    		List<Element> some_detail_lines = new List<Element>();
    		foreach (DetailLine dl in detail_lines)
			{
    			if (dl.LineStyle.Name == "MyNewLineStyle")
    		
    			{
    			
    				some_detail_lines.Add(dl);
    				
    			}

	    			    
			}
    		TaskDialog.Show("Number of Detail Lines of MyNewLineStyle", some_detail_lines.Count.ToString ());
this;
</pre>

**Answer:** Usually easier to filter for objects of GraphicsStyle using ElementClassFilter rather than subcategories of OST_Lines.

GraphicsStyle has a property GraphicsStyle.GraphicsStyleCategory. When this is a subcategory of OST_Lines then it relates to either ModelCurves or DetailCurves (Note that GraphicsStyle.Category is null).

You can't use a class filter for DetailCurves and ModelCurves (which inherit from CurveElement). This base class has the property LineStyle which will be one of the GraphicsStyle elements found above.

When you have items of CurveElement you can distinguish between ModelCurves and DetailCurves as follows:

- DetailCurves have OwnerViewId <> ElementId.InvalidElementId
- ModelCurves have OwnerViewId = ElementId.InvalidElementId

So, you see one potential route is to filter for GraphicsStyles.

Filter again to find GraphicsStyle.GraphicsStyleCategory that is equal to your subcategory of lines.

Then, use this to find CurveElements that have such a CurveElement.LineStyle.

Finally, use CurveElement.OwnerViewId to list either ModelCurves or DetailCurves

One simple way of getting valid GraphicsStyles for DetailCurves/ModelCurves is via CurveElement.GetLineStyleIds (there are many graphics styles that don't relate to lines). Otherwise check GraphicsStyle.GraphicsStyleCategory is a subcategory of OST_Lines.

Example extension methods for getting CurveElements of a given subcategory of lines or matching a GraphicsStyle:

<pre class="code">
 <Extension()>
    Public Function GetLinesOfCategory(Doc As Document, GraphicsStyle As GraphicsStyle, DetailLines As Boolean, Optional FromView As View = Nothing) As List(Of CurveElement)
        Dim FEC As FilteredElementCollector = Nothing
        If FromView Is Nothing Then
            FEC = New FilteredElementCollector(Doc)
        Else
            FEC = New FilteredElementCollector(Doc, FromView)
        End If

        Dim ECF As New ElementCategoryFilter(BuiltInCategory.OST_Lines)
        Dim Els As List(Of CurveElement) = FEC.WherePasses(ECF).WhereElementIsNotElementType.ToElements _
            .Cast(Of CurveElement).Where(Function(x) x.LineStyle.Id = GraphicsStyle.Id _
            AndAlso (x.OwnerViewId <> ElementId.InvalidElementId) = DetailLines).ToList

        Return Els
    End Function

    <Extension()>
    Public Function GetLinesOfCategory(Doc As Document, Category As Category, DetailLines As Boolean, Optional FromView As View = Nothing) As List(Of CurveElement)

        Dim FECgs As New FilteredElementCollector(Doc)
        Dim ECFgs As New ElementClassFilter(GetType(GraphicsStyle))
        Dim Gs As List(Of GraphicsStyle) = FECgs.WherePasses(ECFgs).ToElements _
            .Cast(Of GraphicsStyle).Where(Function(x) x.GraphicsStyleCategory.Id = Category.Id).ToList

        If Gs.Count = 0 Then Return New List(Of CurveElement) Else

        Return GetLinesOfCategory(Doc, Gs(0), DetailLines, FromView)
    End Function
</pre>

I should also say you could probably use:

- FilteredElementCollector.WhereElementIsViewIndependent

In combination with .Excluding to find ModelCurves and Exclude them from your DetailCurves.

I.e., filtering this way first will be quicker since it happens at lower level prior to Linq but you don't have millions of these elements to sort through anyway.

Also, there is a standard filter for detail/model lines: the `CurveElementFilter`:

<pre class="code">
			Category targetLineStyle ;
			IEnumerable<GraphicsStyle>  gstyles = new FilteredElementCollector(doc)
				.OfClass(typeof(GraphicsStyle))
				.Cast<GraphicsStyle>()
				.Where(gs=> gs.GraphicsStyleCategory.Id.IntegerValue == targetLineStyle.Id.IntegerValue);
			ElementId  targetGraphicsStyleId = gstyles.FirstOrDefault().Id;
			CurveElementFilter filter_detail = new CurveElementFilter(CurveElementType.DetailCurve);
			FilterRule frule_typeId = ParameterFilterRuleFactory.CreateEqualsRule(new ElementId(BuiltInParameter.BUILDING_CURVE_GSTYLE),targetGraphicsStyleId);
			ElementParameterFilter filter_type = new ElementParameterFilter(new List<FilterRule>(){ frule_typeId});
			IEnumerable<Element> lines = new FilteredElementCollector(doc)
				.WhereElementIsNotElementType()
				.WhereElementIsCurveDriven()
				.WherePasses(filter_detail)
				.WherePasses(filter_type);
</pre>

**Response:** Great solutions here, thank you so much!

####<a name="3"></a> Revit Project Modification Tracking

Jacob Small shared a powerful collection of nodes
for [Modification Tracking with Dynamo](https://data-shapes.io/2016/12/31/modification-tracking-with-dynamo-and-data-shapes) that
might come in useful for you too.  

####<a name="4"></a> Retrieving all NavisWorks Model Properties

I handled my first NavisWorks .NET API programming ticket, *17292711 &ndash; Most optimized way of getting a complete set of property categories and properties in a model*, and discussed the best approach in depth with the NW development team.

**Question:** When loading an NWD into Navisworks it appears that the categories and properties for all of the items are loaded almost right away when looking at the selection tree. When obtaining this information programmatically it appears it can take much longer. Currently the approach I am using is something like the following:

<pre class="code">
  // iterate over each item in model
  foreach (var item in model.RootItem.DescendantsAndSelf)
  {
    // iterate over each category per item
    foreach (var category in item.PropertyCategories)
    {
      // iterate over each property per category
      foreach (var property in category.Properties)
      {
        // get value/name etc.
        var propName = prop.DisplayName;
        var value = prop.Value;
      }
    }
  }
</pre>

Is there a way that the properties and categories can be programmatically loaded faster?

Is this information cached somewhere? If so, is there a way to access it instead of loading it myself?

Would it be better to use the COM API for performance?

[A] Sadly, there isn't any faster way of reading all the properties using the .NET API.
My little Properties+ tool has to spend a long time searching for properties, but at least it does pop up a progress bar, so that is something the customer could consider doing as well.

We did think about overhauling how we handle properties for NW 2022, but that didn't make the cut. It's possible we might do something for 2023, but no firm plans as of right now.

The code does basically what they are doing, and then just caches the results locally.

Only difference is, it reports progress as well.

There isn’t anything special or confidential about this code, so, as far as I’m concerned, you’re welcome to share it with the customer:

<pre class="code">
  public bool Update()
  {
    if (Nw.Application.MainDocument != null)
    {
      Nw.Search s = new Nw.Search();
      s.SearchConditions.Add(Nw.SearchCondition.HasCategoryByName(Nw.PropertyCategoryNames.Item));
      s.Selection.SelectAll();
      s.Locations = Nw.SearchLocations.DescendantsAndSelf;
      s.PruneBelowMatch = false;
      Nw.ModelItemCollection allItems = s.FindAll(Nw.Application.MainDocument, true);
      Nw.Progress prog = Nw.Application.BeginProgress("Building Property Cache");
      int done = 0;
      int total = allItems.Count;
      foreach (Nw.ModelItem item in allItems)
      {
        foreach (Nw.PropertyCategory cat in item.PropertyCategories)
        {
          foreach (Nw.DataProperty data in cat.Properties)
          {
            m_props.Add(new PropertyDefinition(cat.Name, data.Name, cat.DisplayName, data.DisplayName));
          }
        }
        ++done;
        double percent = (double)done / (double)total;
        if (prog.Update(percent) == false)
        {
          break;
        }
      }
      Nw.Application.EndProgress();
    }
    m_cacheValid = true;
    return true;
  }
</pre>

**Question:** I see that it stores a `PropertyDefinition` for each property encountered, encapsulating the `Name` and `DisplayName` of the category and data items. So, it does not care about the data value.

In a different vein, don’t some objects in the model duplicate others, and their properties as well? Wouldn’t it make sense to identify such objects and reuse the existing data for those? Or is that hard, or impossible?

[A] That code only wants a list of properties, so it doesn't care about the actual values.
And, yes, some objects are duplicates of others, but optimising for that probably won't make much difference.

Summary:

- Use the progress bar
- Cache the results

####<a name="5"></a> Node.js Best Practices

Talking about new non-Revit programming environments, one of the most important topics nowadays is of course [node.js](https://nodejs.org).

For that, I happened upon a really great collection of [Node.js Best Practices](https://github.com/goldbergyoni/nodebestpractices).

This constitutes an invaluable collection of information, and also shows an exemplary solution to present it optimally.

####<a name="6"></a> Node.js Best Practices

Talking about the other and the new, lets not forget to old, either;
for instance, by taking a look
at [the early history of programming and the C language](https://arstechnica.com/features/2020/12/a-damn-stupid-thing-to-do-the-origins-of-c).

