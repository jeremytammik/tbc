using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
public static class correção_revit
{
    public static Element PegarElemento(this Document doc, ElementId id)
    {
        
        Element ele = null;
        //ele = doc.get_Element(id);
        //ele = doc.GetElement(id);
        MethodInfo met = g.doc.GetType().GetMethod("get_Element", new Type[] { typeof(ElementId) });
        if (met == null)
            met = g.doc.GetType().GetMethod("GetElement", new Type[] { typeof(ElementId) });
        ele = met.Invoke(g.doc, new object[] { id }) as Element;
        return ele;
    }
    public static Element PegarElemento(this Document doc, Reference refe)
    {
        Element ele = null;
        ele = doc.GetElement(refe);
        return ele;
    }
    public static char PegarDecimalSymbol(this Document doc)
    {
        char valor = ',';
        MethodInfo met = g.doc.GetType().GetMethod("GetUnits");
        if (met !=null)
        {
            object temp = met.Invoke(g.doc, null);
            PropertyInfo prop = temp.GetType().GetProperty("DecimalSymbol");
            object o = prop.GetValue(temp, null);
            if (o.ToString()=="Comma")
                valor = ',';
            else
                valor = '.';
        }
        else
        {
            object temp = doc.GetType().GetProperty("ProjectUnit");
            PropertyInfo prop = temp.GetType().GetProperty("DecimalSymbolType");
            object o = prop.GetValue(temp,null);
            if (o.ToString() == "DST_COMMA")
                valor = ',';
            else
                valor = '.';
        }
        return valor;
    }
    public static Element Nivel_(this Element ele)
    {
        Element e = null;
        //e = ele.Level;
        //e = PegarElemento(g.doc, ele.LevelId);
        Type t = ele.GetType();
        if (t.GetProperty("Level") != null)
            e = t.GetProperty("Level").GetValue(ele, null) as Element;
        else
            e = g.doc.PegarElemento((ElementId)t.GetProperty("LevelId").GetValue(ele, null));
        return e;
        
    }
    public static List<Material> Materiais_(this Element ele)
    {
        List<Material> mats = new List<Material>();
        //mats = ele.Materials.Cast<Material>().ToList();
        //mats = ele.GetMaterialIds(false).Select(a => PegarElemento(g.doc, a)).Cast<Material>().ToList();
        Type t = ele.GetType();
        if (t.GetProperty("Materials") != null)
            mats = ((IEnumerable)t.GetProperty("Materials").GetValue(ele, null)).Cast<Material>().ToList();
        else
        {
            MethodInfo met = t.GetMethod("GetMaterialIds", new Type[] { typeof(bool) });
            mats = ((ICollection<ElementId>)met.Invoke(ele, new object[] { false })).Select(a => PegarElemento(g.doc, a)).Cast<Material>().ToList();
        }
        return mats;
    }
    public static List<Element> Elementos(this Autodesk.Revit.UI.Selection.Selection sel)
    {
        List<Element> list_elements = new List<Element>();
        //list_elements = sel.Elements.Cast<Element>().ToList();
        //list_elements = sel.PegarElementoIds().ToList().Select(a => PegarElemento(doc, a)).ToList();
        Type t = sel.GetType();
        if (t.GetProperty("PegarElementoIds") != null)
        {
            MethodInfo met = t.GetMethod("PegarElementoIds");
            list_elements = ((ICollection<ElementId>)met.Invoke(sel, null)).Select(a => PegarElemento(g.doc, a)).ToList();

        }
        else
        {
            list_elements = ((SelElementSet)t.GetProperty("Elements").GetValue(sel, null)).Cast<Element>().ToList();

        }
        return list_elements;
    }
    //public static void Clear(this Autodesk.Revit.UI.Selection.Selection sel)
    //{
    //    sel.Elements.Clear();


        
    //}
    public static Parameter PegarParametro_(this Element ele, string nome_paramentro)
    {
        Parameter pa = null;
        //pa = ele.get_Parameter(nome_paramentro);
        //pa = ele.LookupParameter(nome_paramentro);
        Type t = ele.GetType();
        MethodInfo met = t.GetMethod("LookupParameter", new Type[] { typeof(string) });
        if (met == null)
            met = t.GetMethod("get_Parameter", new Type[] { typeof(string) });
        pa = met.Invoke(ele, new object[] { nome_paramentro }) as Parameter;
        return pa;
    }
    public static Parameter PegarParametro_(this Element ele, BuiltInParameter builtInParameter)
    {
        Parameter pa = null;
        //pa = ele.get_Parameter(builtInParameter);
        //pa = ele.LookupParameter(builtInParameter);
        Type t = ele.GetType();
        MethodInfo met = t.GetMethod("LookupParameter", new Type[] { typeof(BuiltInParameter) });
        if (met == null)
            met = t.GetMethod("get_Parameter", new Type[] { typeof(BuiltInParameter) });
        pa = met.Invoke(ele, new object[] { builtInParameter }) as Parameter;
        return pa;
    }
    public static Definition Criar(this Definitions definitions, string nome, ParameterType tipo, bool visibilidade)
    {
        Definition def = null;
        //def = definitions.Create(nome, tipo, visibilidade);
        //ExternalDefinitonCreationOptions ed = new ExternalDefinitonCreationOptions(nome, tipo);
        //ed.Visible = visibilidade;
        //def = definitions.Create(ed);
        List<Type> ls = g.doc.GetType().Assembly.GetTypes().Where(a => a.IsClass && a.Name == "ExternalDefinitonCreationOptions").ToList();

        if (ls.Count > 0)
        {
            Type t = ls[0];

            ConstructorInfo c = t.GetConstructor(new Type[] { typeof(string), typeof(ParameterType) });

            object ed = c.Invoke(new object[] { nome, tipo });

            ed.GetType().GetProperty("Visible").SetValue(ed, visibilidade, null);

            def = definitions.GetType().GetMethod("Create", new Type[] { t }).Invoke(definitions, new object[] { ed }) as Definition;
        }
        else
        {
            def = definitions.GetType().GetMethod("Create", new Type[] { typeof(string), typeof(ParameterType), typeof(bool) }).Invoke(definitions, new object[] { nome, tipo, visibilidade }) as Definition;
        }

        return def;
    }
    public static double PegarMaterialArea_(this Element ele, Material m)
    {
        double valor = 0;
        //valor = ele.GetMaterialArea(m);
        //valor = ele.GetMaterialArea(m.Id, false);
        Type t = ele.GetType();
        MethodInfo met = t.GetMethod("GetMaterialArea", new Type[] { typeof(ElementId), typeof(bool) });
        if (met != null)
        {
            valor = (double)met.Invoke(ele, new object[] { m.Id, false });
        }
        else
        {
            met = t.GetMethod("GetMaterialArea", new Type[] { typeof(Element) });
            valor = (double)met.Invoke(ele, new object[] { m });
        }
        return valor;
    }
    public static XYZ PegarPonto(this Curve curva, int i)
    {
        XYZ p = null;
        //p = curva.GetEndPoint(i);
        //p = curva.PegarPonto(i);
        MethodInfo met = curva.GetType().GetMethod("GetEndPoint", new Type[] { typeof(int) });
        if (met == null)
            met = curva.GetType().GetMethod("get_EndPoint", new Type[] { typeof(int) });
        p = met.Invoke(curva, new object[] { i }) as XYZ;
        return p;
    }
    public static Wall CriarParede(this Document document, Curve curve, ElementId wallTypeId, ElementId levelId, double height, double offset, bool flip, bool structural)
    {
        Wall w = null;
        //w = g.doc.Create.NewWall(curve, (WallType)g.doc.PegarElemento(wallTypeId), (Level)g.doc.PegarElemento(levelId), height, offset, flip, structural);
        //w = Wall.Create(document, curve, wallTypeId, levelId, height, offset, flip, structural);


        object[] parametros = new object[] { document, curve, wallTypeId, levelId, height, offset, flip, structural };
        Type[] tipos = parametros.Select(a => a.GetType()).ToArray();

        MethodInfo met = typeof(Wall).GetMethod("Create", tipos);

        if (met != null)
        {
            w = met.Invoke(null, parametros) as Wall;
        }
        else
        {
            parametros = new object[] { curve, (WallType)g.doc.PegarElemento(wallTypeId), (Level)g.doc.PegarElemento(levelId), height, offset, flip, structural };
            tipos = parametros.Select(a => a.GetType()).ToArray();

            w = g.doc.Create.GetType().GetMethod("NewWall", tipos).Invoke(g.doc.Create, parametros) as Wall;
        }

        return w;
    }
    public static Line CriarLinha(this Document document, XYZ p1, XYZ p2, bool bound = true)
    {
        Line l = null;
        //l = Line.CreateBound(p1, p2);
        //l = g.doc.Application.Create.NewLine(p1, p2, true);

        object[] parametros = new object[] { p1, p2 };
        Type[] tipos = parametros.Select(a => a.GetType()).ToArray();
        string metodo = "CreateBound";
        if (bound == false) metodo = "CreateUnbound";
        MethodInfo met = typeof(Line).GetMethod(metodo, tipos);
        if (met != null)
        {
            l = met.Invoke(null, parametros) as Line;
        }
        else
        {
            parametros = new object[] { p1, p2, bound };
            tipos = parametros.Select(a => a.GetType()).ToArray();
            l = g.doc.Application.Create.GetType().GetMethod("NewLine", tipos).Invoke(g.doc.Application.Create, parametros) as Line;
        }
        return l;
    }
    public static Arc CriarArco(this Document document, XYZ p1, XYZ p2, XYZ p3)
    {
        Arc arc = null;
        //arc = g.doc.Application.Create.NewArc(p1, p2, p3);
        //arc = Arc.Create(p1, p2, p3);
        object[] parametros = new object[] { p1, p2, p3 };
        Type[] tipos = parametros.Select(a => a.GetType()).ToArray();
        string metodo = "Create";
        MethodInfo met = typeof(Arc).GetMethod(metodo, tipos);
        if (met != null)
        {
            arc = met.Invoke(null, parametros) as Arc;
        }
        else
        {
            arc = g.doc.Application.Create.GetType().GetMethod("NewArc", tipos).Invoke(g.doc.Application.Create, parametros) as Arc;
        }
        return arc;
    }
    public static void VirarParede(this Wall wall)
    {
        //wall.flip();
        //wall.Flip();
        string metodo = "Flip";
        MethodInfo met = typeof(Wall).GetMethod(metodo);
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
    public static void DesunirGeometria(this Document document, Element firstElement, Element secondElement)
    {
        try
        {
            //JoinGeometryUtils.UnjoinGeometry(document, firstElement, secondElement);
            List<Type> ls = g.doc.GetType().Assembly.GetTypes().Where(a => a.IsClass && a.Name == "JoinGeometryUtils").ToList();
            object[] parametros = new object[] { document, firstElement, secondElement };
            Type[] tipos = parametros.Select(a => a.GetType()).ToArray();
            if (ls.Count > 0)
            {
                Type t = ls[0];
                MethodInfo met = t.GetMethod("UnjoinGeometry", tipos);
                met.Invoke(null, parametros);
            }
        }
        catch 
        {
            
        }
    }
    public static void UnirGeometria(this Document document, Element firstElement, Element secondElement)
    {
        try
        {
            //JoinGeometryUtils.JoinGeometry(document, firstElement, secondElement);
            List<Type> ls = g.doc.GetType().Assembly.GetTypes().Where(a => a.IsClass && a.Name == "JoinGeometryUtils").ToList();
            object[] parametros = new object[] { document, firstElement, secondElement };
            Type[] tipos = parametros.Select(a => a.GetType()).ToArray();
            if (ls.Count > 0)
            {
                Type t = ls[0];
                MethodInfo met = t.GetMethod("JoinGeometry", tipos);
                met.Invoke(null, parametros);
            }
        }
        catch 
        {
        }
    }
    public static bool EstáUnido(this Document document, Element firstElement, Element secondElement)
    {
        bool b = false;
        //b = JoinGeometryUtils.AreElementsJoined(document, firstElement, secondElement);
        List<Type> ls = g.doc.GetType().Assembly.GetTypes().Where(a => a.IsClass && a.Name == "JoinGeometryUtils").ToList();
        object[] parametros = new object[] { document, firstElement, secondElement };
        Type[] tipos = parametros.Select(a => a.GetType()).ToArray();
        if (ls.Count > 0)
        {
            Type t = ls[0];
            MethodInfo met = t.GetMethod("AreElementsJoined", tipos);
            b = (bool)met.Invoke(null, parametros);
        }
        return b;
    }
    public static ElementId PegarFonte(this Part part)
    {
        ElementId id = null;

        //id = part.GetSourceElementIds().First().HostElementId;
        //id = part.OriginalDividedElementId;

        PropertyInfo prop = part.GetType().GetProperty("OriginalDividedElementId");
        if (prop != null)
            id = prop.GetValue(part, null) as ElementId;
        else
        {
            MethodInfo met = part.GetType().GetMethod("GetSourceElementIds");
            object temp = met.Invoke(part, null);
            met = temp.GetType().GetMethod("First");
            temp = met.Invoke(temp, null);
            prop = temp.GetType().GetProperty("HostElementId");
            id = prop.GetValue(temp, null) as ElementId;
        }
        

        return id;
    }
    public static List<GeometryObject> PegarObjetosGeometricos(this Element ele)
    {
        List<GeometryObject> list = new List<GeometryObject>();

        try
        {
            //geometryElement;
            //geometryElement.Objects;
            Options op = new Options();
            object obj = ele.get_Geometry(op);
            PropertyInfo prop = obj.GetType().GetProperty("Objects");
            if (prop != null)
            {
                obj = prop.GetValue(obj, null);
                IEnumerable arr = obj as IEnumerable;
                foreach (GeometryObject geo in arr)
                {
                    list.Add(geo);
                }
            }
            else
            {
                IEnumerable<GeometryObject> geos = obj as IEnumerable<GeometryObject>;
                foreach (var geo in geos)
                {
                    list.Add(geo);
                }
            }


            //System.Windows.Forms.MessageBox.Show(list.Count.ToString());
        }
        catch 
            //(Exception ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Message + ex.StackTrace);
            //mbox.TryCatch(ex);
        }

        return list;
    }
    public static bool CalcularVolumeDeArea(this Document doc, bool b)
    {
        //doc.Settings.VolumeCalculationSetting.VolumeCalculationOptions.VolumeComputationEnable = b;
        //AreaVolumeSettings.GetAreaVolumeSettings(doc).ComputeVolumes = b;

        try
        {
            List<Type> ls = g.doc.GetType().Assembly.GetTypes().Where(a => a.IsClass && a.Name == "AreaVolumeSettings").ToList();
            if (ls.Count > 0)
            {
                Type t = ls[0];

                object[] parametros = new object[] { doc };
                Type[] tipos = parametros.Select(a => a.GetType()).ToArray();
                MethodInfo met = t.GetMethod("GetAreaVolumeSettings", tipos);
                object temp = met.Invoke(null, parametros);
                temp.GetType().GetProperty("ComputeVolumes").SetValue(temp, b, null);
            }
            else
            {
                PropertyInfo prop = doc.Settings.GetType().GetProperty("VolumeCalculationSetting");
                object temp = prop.GetValue(doc.Settings, null);
                prop = temp.GetType().GetProperty("VolumeCalculationOptions");
                temp = prop.GetValue(temp, null);
                prop = temp.GetType().GetProperty("VolumeComputationEnable");
                prop.SetValue(temp, b, null);
            }
        }
        catch (Exception ex)
        {

            System.Windows.Forms.MessageBox.Show(ex.Message+ex.StackTrace);
        }

        return b;
    }
    public static void SetarSeleção(this Selection sel , ICollection<ElementId> elementos)
    {
        //sel.Elements.Clear();

        sel.LimparSeleção();
        

        //uidoc.Selection.Elements.Add(g.doc.PegarElemento(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as ElementId));

        //SelElementSet aa;
        //sel.SetElementIds();

         object[] parametros = new object[] { elementos };
         Type[] tipos = parametros.Select(a => a.GetType()).ToArray();

        MethodInfo met = sel.GetType().GetMethod("SetElementIds",tipos);

        if (met!=null)
        {
            met.Invoke(sel, parametros);
        }
        else
        {
            PropertyInfo prop = sel.GetType().GetProperty("Elements");
            object temp = prop.GetValue(sel, null);

            if (elementos.Count==0)
            {
                met = temp.GetType().GetMethod("Clear");
                met.Invoke(temp, null);
            }
            else
            {
                foreach (ElementId id in elementos)
                {
                    Element elemento = g.doc.PegarElemento(id);


                    parametros = new object[] { elemento };
                    tipos = parametros.Select(a => a.GetType()).ToArray();
                    met = temp.GetType().GetMethod("Add",tipos);
                    met.Invoke(temp, parametros);
                }
            }
            
        }




    }
    public static void LimparSeleção(this Selection sel)
    {
        //sel.Elements.Clear();
        //sel.SetElementIds(new List<ElementId>());
        PropertyInfo prop = sel.GetType().GetProperty("Elements");
        if (prop != null)
        {
            object obj = prop.GetValue(sel,null);
            MethodInfo met = obj.GetType().GetMethod("Clear");
            met.Invoke(obj, null);
        }
        else
        {
            ICollection<ElementId> ids = new List<ElementId>();
            MethodInfo met = sel.GetType().GetMethod("",new Type[]{ids.GetType()});
            met.Invoke(sel, new object[] { ids });
        }
    }

    public static ICollection<ElementId> PegarSeleção(this Selection sel)
    {
        return null;

    }

    public static void Deletar(this Document doc, Element ele)
    {
        //g.doc.Delete(ele);
        //g.doc.Delete(ele.Id);

        object obj = g.doc;
        MethodInfo met = obj.GetType().GetMethod("Delete",new Type[]{ typeof(Element)});
        if (met!=null)
        {
            met.Invoke(obj, new object[] { ele });
        }
        else
        {
            met = obj.GetType().GetMethod("Delete", new Type[] { typeof(ElementId) });
            met.Invoke(obj, new object[] { ele.Id });
        }
    }

    public static ElementId Duplicar(this View v)
    {
        ElementId id = null;

        //id = v.Duplicate(ViewDuplicateOption.WithDetailing);

        try
        {

            //JoinGeometryUtils.UnjoinGeometry(document, firstElement, secondElement);
            List<Type> ls = g.doc.GetType().Assembly.GetTypes().Where(a => a.IsEnum && a.Name == "ViewDuplicateOption").ToList();
           
            //if (ls.Count > 0)
            //{
            //    Type t = ls[0];

            //    MemberInfo[] memberInfos = t.GetMembers(BindingFlags.Public | BindingFlags.Static);
            //    string alerta = "";
            //    for (int i = 0; i < memberInfos.Length; i++)
            //    {
            //        //WithDetailing;
            //        alerta += memberInfos[i].Name + " - ";
            //        alerta += memberInfos[i].GetType().Name + "\n";
            //    }


            //    object obj = v;
            //    MethodInfo met = v.GetType().GetMethod("Duplicate", new Type[] { typeof(ViewDuplicateOption) });
            //    if (met != null)
            //    {
            //        id = met.Invoke(obj, new object[] { ViewDuplicateOption.WithDetailing }) as ElementId;
            //    }
            //}




          
        }
        catch (Exception ex)
        {
            
            //mbox.TryCatch(ex);
        }

        return id;

    }
    
}
