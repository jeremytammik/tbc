using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
public static class CompatibiltyMethods
{

	#region curve
	public static XYZ GetPoint2(this Curve curva,
     int i)
    {
        XYZ value = null;
        try
        {
            MethodInfo met = curva.GetType()
            .GetMethod("GetEndPoint", new Type[] { typeof(int) });
            if (met == null)
                met = curva.GetType()
                .GetMethod("get_EndPoint", new Type[] { typeof(int) });
            value = met.Invoke(curva,
             new object[] { i }) as XYZ;
        }
        catch{}
        return value;
    }
	#endregion // curve

	#region definitions
	public static Definition Create2(this 
    Definitions definitions,Document doc, string nome,
     ParameterType tipo, bool visibilidade)
    {
        Definition value = null;
        try
        {
            List<Type> ls = doc.GetType().Assembly
            .GetTypes().Where(a => a.IsClass && a
            .Name == "ExternalDefinitonCreationOptions").ToList();
            if (ls.Count > 0)
            {
                Type t = ls[0];
                ConstructorInfo c = t
                .GetConstructor(new Type[] { typeof(string),
                 typeof(ParameterType) });
                object ed = c
                .Invoke(new object[] { nome, tipo });
                ed.GetType().GetProperty("Visible")
                .SetValue(ed, visibilidade, null);
                value = definitions.GetType()
                .GetMethod("Create", new Type[] { t }).Invoke(definitions,
                 new object[] { ed }) as Definition;
            }
            else
            {
                value = definitions.GetType()
                .GetMethod("Create", new Type[] { typeof(string),
                 typeof(ParameterType), typeof(bool) }).Invoke(definitions,
                 new object[] { nome, tipo,
                 visibilidade }) as Definition;
            }
        }
        catch{}
        return value;
    }
	#endregion // definitions

	#region document
	public static Element GetElement2(this Document 
    doc, ElementId id)
    {
        Element value = null;
        try
        {
            MethodInfo met = doc.GetType()
            .GetMethod("get_Element", new Type[] { typeof(ElementId) });
            if (met == null)
                met = doc.GetType()
                .GetMethod("GetElement", new Type[] { typeof(ElementId) });
            value = met.Invoke(doc,
             new object[] { id }) as Element;
        }
        catch{}
        return value;
    }
    public static Element GetElement2(this Document 
    doc, Reference refe)
    {
        Element value = null;
        try
        {
            value = doc.GetElement(refe);
        }
        catch{}
        return value;
    }
    public static Line CreateLine2(this Document doc,
     XYZ p1, XYZ p2, bool bound = true)
    {
        Line value = null;
        try
        {
            object[] parametros = new object[] { p1,
             p2 };
            Type[] tipos = parametros.Select(a => a
            .GetType()).ToArray();
            string metodo = "CreateBound";
            if (bound == false) metodo = 
            "CreateUnbound";
            MethodInfo met = typeof(Line)
            .GetMethod(metodo, tipos);
            if (met != null)
            {
                value = met.Invoke(null,
                 parametros) as Line;
            }
            else
            {
                parametros = new object[] { p1, p2,
                 bound };
                tipos = parametros.Select(a => a
                .GetType()).ToArray();
                value = doc.Application.Create
                .GetType().GetMethod("NewLine", tipos).Invoke(doc
                .Application.Create, parametros) as Line;
            }
        }
        catch{}
        return value;
    }
    public static Wall CreateWall2(this Document doc,
     Curve curve, ElementId wallTypeId,
     ElementId levelId, double height, double offset, bool flip,
     bool structural)
    {
        Wall value = null;
        try
        {
            object[] parametros = new object[] { doc,
             curve, wallTypeId, levelId, height, offset, flip,
             structural };
            Type[] tipos = parametros.Select(a => a
            .GetType()).ToArray();
            MethodInfo met = typeof(Wall)
            .GetMethod("Create", tipos);
            if (met != null)
            {
                value = met.Invoke(null,
                 parametros) as Wall;
            }
            else
            {
                parametros = new object[] { curve,
                 (WallType)doc.GetElement2(wallTypeId), (Level)doc
                .GetElement2(levelId), height, offset, flip,
                 structural };
                tipos = parametros.Select(a => a
                .GetType()).ToArray();
                value = doc.Create.GetType()
                .GetMethod("NewWall", tipos).Invoke(doc.Create,
                 parametros) as Wall;
            }
        }
        catch{}
        return value;
    }
    public static Arc CreateArc2(this Document doc,
     XYZ p1, XYZ p2, XYZ p3)
    {
        Arc value = null;
        try
        {
            object[] parametros = new object[] { p1,
             p2, p3 };
            Type[] tipos = parametros.Select(a => a
            .GetType()).ToArray();
            string metodo = "Create";
            MethodInfo met = typeof(Arc)
            .GetMethod(metodo, tipos);
            if (met != null)
            {
                value = met.Invoke(null,
                 parametros) as Arc;
            }
            else
            {
                value = doc.Application.Create
                .GetType().GetMethod("NewArc", tipos).Invoke(doc
                .Application.Create, parametros) as Arc;
            }
        }
        catch{}
        return value;
    }
    public static char GetDecimalSymbol2(this 
    Document doc)
    {
        char valor = ',';
        try
        {
            MethodInfo met = doc.GetType()
            .GetMethod("GetUnits");
            if (met != null)
            {
                object temp = met.Invoke(doc, null);
                PropertyInfo prop = temp.GetType()
                .GetProperty("DecimalSymbol");
                object o = prop.GetValue(temp, null);
                if (o.ToString() == "Comma")
                    valor = ',';
                else
                    valor = '.';
            }
            else
            {
                object temp = doc.GetType()
                .GetProperty("ProjectUnit").GetValue(doc,null);
                PropertyInfo prop = temp.GetType()
                .GetProperty("DecimalSymbolType");
                object o = prop.GetValue(temp, null);
                if (o.ToString() == "DST_COMMA")
                    valor = ',';
                else
                    valor = '.';
            }
        }
        catch{}
        return valor;
    }
    public static void UnjoinGeometry2(this Document 
    doc, Element firstElement, Element secondElement)
    {
        try
        {
            List<Type> ls = doc.GetType().Assembly
            .GetTypes().Where(a => a.IsClass && a
            .Name == "JoinGeometryUtils").ToList();
            object[] parametros = new object[] { doc,
             firstElement, secondElement };
            Type[] tipos = parametros.Select(a => a
            .GetType()).ToArray();
            if (ls.Count > 0)
            {
                Type t = ls[0];
                MethodInfo met = t
                .GetMethod("UnjoinGeometry", tipos);
                met.Invoke(null, parametros);
            }
        }
        catch{}
    }
    public static void JoinGeometry2(this Document 
    doc, Element firstElement, Element secondElement)
    {
        try
        {
            List<Type> ls = doc.GetType().Assembly
            .GetTypes().Where(a => a.IsClass && a
            .Name == "JoinGeometryUtils").ToList();
            object[] parametros = new object[] { doc,
             firstElement, secondElement };
            Type[] tipos = parametros.Select(a => a
            .GetType()).ToArray();
            if (ls.Count > 0)
            {
                Type t = ls[0];
                MethodInfo met = t
                .GetMethod("JoinGeometry", tipos);
                met.Invoke(null, parametros);
            }
        }
        catch{}
    }
    public static bool IsJoined2(this Document doc,
     Element firstElement, Element secondElement)
    {
        bool value = false;
        try
        {
            List<Type> ls = doc.GetType().Assembly
            .GetTypes().Where(a => a.IsClass && a
            .Name == "JoinGeometryUtils").ToList();
            object[] parametros = new object[] { doc,
             firstElement, secondElement };
            Type[] tipos = parametros.Select(a => a
            .GetType()).ToArray();
            if (ls.Count > 0)
            {
                Type t = ls[0];
                MethodInfo met = t
                .GetMethod("AreElementsJoined", tipos);
                value = (bool)met.Invoke(null,
                 parametros);
            }
        }
        catch{}
        return value;
    }
    public static bool CalculateVolumeArea2(this 
    Document doc, bool value)
    {
        try
        {
            List<Type> ls = doc.GetType().Assembly
            .GetTypes().Where(a => a.IsClass && a
            .Name == "AreaVolumeSettings").ToList();
            if (ls.Count > 0)
            {
                Type t = ls[0];
                object[] parametros = new object[] { 
                doc };
                Type[] tipos = parametros
                .Select(a => a.GetType()).ToArray();
                MethodInfo met = t
                .GetMethod("GetAreaVolumeSettings", tipos);
                object temp = met.Invoke(null,
                 parametros);
                temp.GetType()
                .GetProperty("ComputeVolumes").SetValue(temp, value, null);
            }
            else
            {
                PropertyInfo prop = doc.Settings
                .GetType().GetProperty("VolumeCalculationSetting");
                object temp = prop.GetValue(doc
                .Settings, null);
                prop = temp.GetType()
                .GetProperty("VolumeCalculationOptions");
                temp = prop.GetValue(temp, null);
                prop = temp.GetType()
                .GetProperty("VolumeComputationEnable");
                prop.SetValue(temp, value, null);
            }
        }
        catch{}
        return value;
    }
    public static Group CreateGroup2(this Document 
    doc, List<Element> elementos)
    {
        Group value = null;
        try
        {
            ElementSet eleset = new ElementSet();
            foreach (Element ele in elementos)
            {
                eleset.Insert(ele);
            }
            ICollection<ElementId> col = elementos
            .Select(a => a.Id).ToList();
            object obj = doc.Create;
            MethodInfo met = obj.GetType()
            .GetMethod("NewGroup", new Type[] { col.GetType() });
            if (met != null)
            {
                met.Invoke(obj, new object[] { col });
            }
            else
            {
                met = obj.GetType()
                .GetMethod("NewGroup", new Type[] { eleset.GetType() });
                met.Invoke(obj,
                 new object[] { eleset });
            }
        }
        catch{}
        return value;
    }
    public static void Delete2(this Document doc,
     Element ele)
    {
        try
        {
            object obj = doc;
            MethodInfo met = obj.GetType()
            .GetMethod("Delete", new Type[] { typeof(Element) });
            if (met != null)
            {
                met.Invoke(obj, new object[] { ele });
            }
            else
            {
                met = obj.GetType()
                .GetMethod("Delete", new Type[] { typeof(ElementId) });
                met.Invoke(obj, new object[] { ele
                .Id });
            }
        }
        catch{}
    }
	#endregion // document

	#region element
	public static Element Level2(this Element ele)
    {
        Element value = null;
        try
        {
            Document doc = ele.Document;
            Type t = ele.GetType();
            if (t.GetProperty("Level") != null)
                value = t.GetProperty("Level")
                .GetValue(ele, null) as Element;
            else
                value = doc.GetElement2((ElementId)t
                .GetProperty("LevelId").GetValue(ele, null));
        }
        catch{}
        return value;
    }
    public static List<Material> Materiais2(this 
    Element ele)
    {
        List<Material> value = new List<Material>();
        try
        {
            Document doc = ele.Document;
            Type t = ele.GetType();
            if (t.GetProperty("Materials") != null)
                value = ((IEnumerable)t
                .GetProperty("Materials").GetValue(ele, null)).Cast<Material>()
                .ToList();
            else
            {
                MethodInfo met = t
                .GetMethod("GetMaterialIds", new Type[] { typeof(bool) });
                value = ((ICollection<ElementId>)met
                .Invoke(ele, new object[] { false }))
                .Select(a => doc.GetElement2(a)).Cast<Material>().ToList();
            }
        }
        catch{}
        return value;
    }
    public static Parameter GetParameter2(this 
    Element ele, string nome_paramentro)
    {
        Parameter value = null;
        try
        {
            Type t = ele.GetType();
            MethodInfo met = t
            .GetMethod("LookupParameter", new Type[] { typeof(string) });
            if (met == null)
                met = t.GetMethod("get_Parameter",
                 new Type[] { typeof(string) });
            value = met.Invoke(ele,
             new object[] { nome_paramentro }) as Parameter;
            if (value == null)
            {
                var pas = ele.Parameters
                .Cast<Parameter>().ToList();
                if (pas.Exists(a => a.Definition
                .Name.ToLower() == nome_paramentro.Trim().ToLower()))
                    value = pas.First(a => a
                    .Definition.Name.ToLower() == nome_paramentro.Trim()
                    .ToLower());
            }
        }
        catch{}
        return value;
    }
    public static Parameter GetParameter2(this 
    Element ele, BuiltInParameter builtInParameter)
    {
        Parameter value = null;
        try
        {
            Type t = ele.GetType();
            MethodInfo met = t
            .GetMethod("LookupParameter", new Type[] { typeof(BuiltInParameter) });
            if (met == null)
                met = t.GetMethod("get_Parameter",
                 new Type[] { typeof(BuiltInParameter) });
            value = met.Invoke(ele,
             new object[] { builtInParameter }) as Parameter;
        }
        catch{}
        return value;
    }
    public static double GetMaterialArea2(this 
    Element ele, Material m)
    {
        double value = 0;
        try
        {
            Type t = ele.GetType();
            MethodInfo met = t
            .GetMethod("GetMaterialArea", new Type[] { typeof(ElementId),
             typeof(bool) });
            if (met != null)
            {
                value = (double)met.Invoke(ele,
                 new object[] { m.Id, false });
            }
            else
            {
                met = t.GetMethod("GetMaterialArea",
                 new Type[] { typeof(Element) });
                value = (double)met.Invoke(ele,
                 new object[] { m });
            }
        }
        catch{}
        return value;
    }
    public static double GetMaterialVolume2(this 
    Element ele, Material m)
    {
        double value = 0;
        try
        {
            Type t = ele.GetType();
            MethodInfo met = t
            .GetMethod("GetMaterialVolume", new Type[] { typeof(ElementId),
             typeof(bool) });
            if (met != null)
            {
                value = (double)met.Invoke(ele,
                 new object[] { m.Id, false });
            }
            else
            {
                met = t
                .GetMethod("GetMaterialVolume", new Type[] { typeof(ElementId) });
                value = (double)met.Invoke(ele,
                 new object[] { m.Id });
            }
        }
        catch{}
        return value;
    }
    public static List<GeometryObject> 
    GetGeometricObjects2(this Element ele)
    {
        List<GeometryObject> value = 
        new List<GeometryObject>();
        try
        {
            Options op = new Options();
            object obj = ele.get_Geometry(op);
            PropertyInfo prop = obj.GetType()
            .GetProperty("Objects");
            if (prop != null)
            {
                obj = prop.GetValue(obj, null);
                IEnumerable arr = obj as IEnumerable;
                foreach (GeometryObject geo in arr)
                {
                    value.Add(geo);
                }
            }
            else
            {
                IEnumerable<GeometryObject> geos = 
                obj as IEnumerable<GeometryObject>;
                foreach (var geo in geos)
                {
                    value.Add(geo);
                }
            }
        }
        catch{}
        return value;
    }
	#endregion // element

	#region familysymbol
	public static void EnableFamilySymbol2(this 
    FamilySymbol fsymbol)
    {
        try
        {
            MethodInfo met = fsymbol.GetType()
            .GetMethod("Activate");
            if (met != null)
            {
                met.Invoke(fsymbol, null);
            }
        }
        catch{}
    }
	#endregion // familysymbol

	#region internaldefiniton
	public static void VaryGroup2(this 
    InternalDefinition def,Document doc)
    {
        try
        {
            object[] parametros = new object[] { doc,
             true };
            Type[] tipos = parametros.Select(a => a
            .GetType()).ToArray();
            MethodInfo met = def.GetType()
            .GetMethod("SetAllowVaryBetweenGroups", tipos);
            if (met != null)
            {
                met.Invoke(def, parametros);
            }
        }
        catch{}
    }
	#endregion // internaldefiniton

	#region part
	public static ElementId GetSource2(this Part part)
    {
        ElementId value = null;
        try
        {
            PropertyInfo prop = part.GetType()
            .GetProperty("OriginalDividedElementId");
            if (prop != null)
                value = prop.GetValue(part,
                 null) as ElementId;
            else
            {
                MethodInfo met = part.GetType()
                .GetMethod("GetSourceElementIds");
                object temp = met.Invoke(part, null);
                met = temp.GetType()
                .GetMethod("First");
                temp = met.Invoke(temp, null);
                prop = temp.GetType()
                .GetProperty("HostElementId");
                value = prop.GetValue(temp,
                 null) as ElementId;
            }
        }
        catch{}
        return value;
    }
	#endregion // part

	#region selection
	public static List<Element> GetSelection2(this 
    Selection sel,Document doc)
    {
        List<Element> value = new List<Element>();
        try
        {
            sel.GetElementIds();
            Type t = sel.GetType();
            if (t.GetMethod("GetElementIds") != null)
            {
                MethodInfo met = t
                .GetMethod("GetElementIds");
                value = ((ICollection<ElementId>)met
                .Invoke(sel, null)).Select(a => doc.GetElement2(a))
                .ToList();
            }
            else
            {
                value = ((IEnumerable)t
                .GetProperty("Elements").GetValue(sel, null)).Cast<Element>()
                .ToList();
            }
        }
        catch{}
        return value;
    }
    public static void SetSelection2(this Selection 
    sel,Document doc, ICollection<ElementId> elementos)
    {
        try
        {
            sel.ClearSelection2();
            object[] parametros = new object[] { 
            elementos };
            Type[] tipos = parametros.Select(a => a
            .GetType()).ToArray();
            MethodInfo met = sel.GetType()
            .GetMethod("SetElementIds", tipos);
            if (met != null)
            {
                met.Invoke(sel, parametros);
            }
            else
            {
                PropertyInfo prop = sel.GetType()
                .GetProperty("Elements");
                object temp = prop.GetValue(sel, null);
                if (elementos.Count == 0)
                {
                    met = temp.GetType()
                    .GetMethod("Clear");
                    met.Invoke(temp, null);
                }
                else
                {
                    foreach (ElementId id in elementos)
                    {
                        Element elemento = doc
                        .GetElement2(id);
                        parametros = new object[] { 
                        elemento };
                        tipos = parametros
                        .Select(a => a.GetType()).ToArray();
                        met = temp.GetType()
                        .GetMethod("Add", tipos);
                        met.Invoke(temp, parametros);
                    }
                }
            }
        }
        catch{}
    }
    public static void ClearSelection2(this 
    Selection sel)
    {
        try
        {
            PropertyInfo prop = sel.GetType()
            .GetProperty("Elements");
            if (prop != null)
            {
                object obj = prop.GetValue(sel, null);
                MethodInfo met = obj.GetType()
                .GetMethod("Clear");
                met.Invoke(obj, null);
            }
            else
            {
                ICollection<ElementId> ids = 
                new List<ElementId>();
                MethodInfo met = sel.GetType()
                .GetMethod("SetElementIds", new Type[] { ids.GetType() });
                met.Invoke(sel, new object[] { ids });
            }
        }
        catch{}
    }
	#endregion // selection

	#region uiapplication
	public static System.Drawing
    .Rectangle GetDrawingArea2(this UIApplication ui)
    {
        System.Drawing.Rectangle value = System
        .Windows.Forms.Screen.PrimaryScreen.Bounds;
        try
        {
        }
        catch{}
        return value;
    }
	#endregion // uiapplication

	#region view
	public static ElementId Duplicate2(this View view)
    {
        ElementId value = null;
        try
        {
            Document doc = view.Document;
            List<Type> ls = doc.GetType().Assembly
            .GetTypes().Where(a => a.IsEnum && a
            .Name == "ViewDuplicateOption").ToList();
            if (ls.Count > 0)
            {
                Type t = ls[0];
                object obj = view;
                MethodInfo met = view.GetType()
                .GetMethod("Duplicate", new Type[] { t });
                if (met != null)
                {
                    value = met.Invoke(obj,
                     new object[] { 2 }) as ElementId;
                }
            }
        }
        catch{}
        return value;
    }
    public static void SetOverlayView2(this View 
    view, List<ElementId> ids, Color cor = null,
     int espessura = -1)
    {
        try
        {
            Document doc = view.Document;
            List<Type> ls = doc.GetType().Assembly
            .GetTypes().Where(a => a.IsClass && a
            .Name == "OverrideGraphicSettings").ToList();
            if (ls.Count > 0)
            {
                Type t = ls[0];
                ConstructorInfo construtor = t
                .GetConstructor(new Type[] { });
                construtor.Invoke(new object[] { });
                object obj = construtor
                .Invoke(new object[] { });
                MethodInfo met = obj.GetType()
                .GetMethod("SetProjectionLineColor", new Type[] { cor
                .GetType() });
                met.Invoke(obj, new object[] { cor });
                met = obj.GetType()
                .GetMethod("SetProjectionLineWeight", new Type[] { espessura
                .GetType() });
                met.Invoke(obj,
                 new object[] { espessura });
                met = view.GetType()
                .GetMethod("SetElementOverrides", new Type[] { typeof(ElementId),
                 obj.GetType() });
                foreach (ElementId id in ids)
                {
                    met.Invoke(view,
                     new object[] { id, obj });
                }
            }
            else
            {
                MethodInfo met = view.GetType()
                .GetMethod("set_ProjColorOverrideByElement",
                 new Type[] { typeof(ICollection<ElementId>), typeof(Color) });
                met.Invoke(view, new object[] { ids,
                 cor });
                met = view.GetType()
                .GetMethod("set_ProjLineWeightOverrideByElement",
                 new Type[] { typeof(ICollection<ElementId>), typeof(int) });
                met.Invoke(view, new object[] { ids,
                 espessura });
            }
        }
        catch{}
    }
	#endregion // view

	#region viewplan
	public static ElementId GetViewTemplateId2(this 
    ViewPlan view)
    {
        ElementId value = null;
        try
        {
            PropertyInfo prop = view.GetType()
            .GetProperty("ViewTemplateId");
            if (prop != null)
            {
                value = prop.GetValue(view,
                 null) as ElementId;
            }
        }
        catch{}
        return value;
    }
    public static void SetViewTemplateId2(this 
    ViewPlan view, ElementId id)
    {
        try
        {
            PropertyInfo prop = view.GetType()
            .GetProperty("ViewTemplateId");
            if (prop != null)
            {
                prop.SetValue(view, id, null);
            }
        }
        catch{}
    }
	#endregion // viewplan

	#region wall
	public static void FlipWall2(this Wall wall)
    {
        try
        {
            string metodo = "Flip";
            MethodInfo met = typeof(Wall)
            .GetMethod(metodo);
            if (met != null)
            {
                met.Invoke(wall, null);
            }
            else
            {
                metodo = "flip";
                met = typeof(Wall).GetMethod(metodo);
                met.Invoke(wall, null);
            }
        }
        catch{}
    }
	#endregion // wall
}