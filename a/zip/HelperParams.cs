using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO; // For SpecialDirectories
//
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
//
using AppRvt = Autodesk.Revit.ApplicationServices.Application; // to avoid ambiguities
using DocRvt = Autodesk.Revit.DB.Document; // to avoid ambiguities
using BindingRvt = Autodesk.Revit.DB.Binding; // to avoid ambiguities


namespace MS.Revit.Utils
{
    /// <summary>
    /// Major Helper Class for Revit Params
    /// </summary>
    public static class HelperParams
    {
        public enum BindSharedParamResult
        {
            eAlreadyBound,
            eSuccessfullyBound,
            eWrongParamType,
            //eWrongCategory, //not exposed
            //eWrongVisibility, //not exposed
            eWrongBindingType,
            eFailed
        }

        /// <summary>
        /// Get Element Parameter *by name*. By defualt NOT case sensitive. Use overloaded one if case sensitive needed.
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Parameter GetElemParam(Element elem, string name)
        {
            return GetElemParam(elem, name, false);
        }
        public static Parameter GetElemParam(Element elem, string name, bool matchCase)
        {
            StringComparison comp = StringComparison.CurrentCultureIgnoreCase;
            if (matchCase) comp = StringComparison.CurrentCulture;

            foreach (Parameter p in elem.Parameters)
            {
                if (p.Definition.Name.Equals(name, comp)) return p;
            }
            // if here, not found
            return null;
        }

        /// <summary>
        /// Get or Create Shared Params File
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static DefinitionFile GetOrCreateSharedParamsFile(AppRvt app)
        {
            string fileName = string.Empty;
            try // generic
            {
                // Get file
                fileName = app.SharedParametersFilename;
                // Create file if not set yet (ie after Revit installed and no Shared params used so far)
                if (string.Empty == fileName)
                {
                    fileName = SpecialDirectories.MyDocuments + "\\MyRevitSharedParams.txt";
                    StreamWriter stream = new StreamWriter(fileName);
                    stream.Close();
                    app.SharedParametersFilename = fileName;
                }
                return app.OpenSharedParameterFile();
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR: Failed to get or create Shared Params File: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Get or Create Shared Parameters Group
        /// </summary>
        /// <param name="defFile"></param>
        /// <param name="grpName"></param>
        /// <returns></returns>
        public static DefinitionGroup GetOrCreateSharedParamsGroup(DefinitionFile defFile, string grpName)
        {
            try // generic
            {
                DefinitionGroup defGrp = defFile.Groups.get_Item(grpName);
                if (null == defGrp) defGrp = defFile.Groups.Create(grpName);
                return defGrp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("ERROR: Failed to get or create Shared Params Group: {0}", ex.Message));
                return null;
            }
        }

        /// <summary> 
        /// Get or Create Shared Parameter Definition
        /// </summary>
        /// <param name="defGrp"></param>
        /// <param name="parType">used only if creating</param>
        /// <param name="parName">used only if creating</param>
        /// <param name="visible">used only if creating</param>
        /// <returns></returns>
        public static Definition GetOrCreateSharedParamDefinition(DefinitionGroup defGrp, ParameterType parType, string parName, bool visible)
        {
            try // generic
            {
                Definition def = defGrp.Definitions.get_Item(parName);
                if (null == def) def = defGrp.Definitions.Create(parName, parType, visible);
                return def;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("ERROR: Failed to get or create Shared Params Definition: {0}", ex.Message));
                return null;
            }
        }

        /// <summary>
        /// Gets or Creates Element's shared param.
        /// </summary>
        /// <param name="elem">Revit Element to get param for</param>
        /// <param name="paramName">Parameter Name</param>
        /// <param name="grpName">Param Group Name (relevant only when Creation takes place)</param>
        /// <param name="paramType">Param Type (relevant only when Creation takes place)</param>
        /// <param name="visible">Param UI Visibility (relevant only when Creation takes place)</param>
        /// <param name="instanceBinding">Param Binding: Instance or Type (relevant only when Creation takes place)</param>
        /// <returns></returns>
        public static Parameter GetOrCreateElemSharedParam(Element elem, string paramName, string grpName, ParameterType paramType, bool visible, bool instanceBinding)
        {
            try
            {
                // Check if existing
                Parameter param = GetElemParam(elem, paramName);
                if (null != param) return param;

                // If here, need to create it...
                BindSharedParamResult res = BindSharedParam(elem.Document, elem.Category, paramName, grpName, paramType, visible, instanceBinding);
                if (res != BindSharedParamResult.eSuccessfullyBound && res != BindSharedParamResult.eAlreadyBound) return null;

                // If here, binding is OK and param seems to be IMMEDIATELY available from the very same command
                return GetElemParam(elem, paramName);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(string.Format("Error in getting or creating Element Param: {0}", ex.Message));
                return null;
            }
        }

        /// <summary>
        /// Gets or Creates Project Information (per-doc) shared param
        /// </summary>
        /// <param name="doc">Revit Document</param>
        /// <param name="paramName">Parameter Name</param>
        /// <param name="grpName">Param Group Name (relevant only when Creation takes place)</param>
        /// <param name="paramType">Param Type (relevant only when Creation takes place)</param>
        /// <param name="visible">Param UI Visibility (relevant only when Creation takes place)</param>
        /// <returns></returns>
        public static Parameter GetOrCreateProjInfoSharedParam(DocRvt doc, string paramName, string grpName, ParameterType paramType, bool visible)
        {
            // Just delegate the call using ProjectInfo Element
            return GetOrCreateElemSharedParam(doc.ProjectInformation, paramName, grpName, paramType, visible, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="cat"></param>
        /// <param name="paramName"></param>
        /// <param name="grpName"></param>
        /// <param name="paramType"></param>
        /// <param name="visible"></param>
        /// <param name="instanceBinding"></param>
        /// <returns></returns>
        public static BindSharedParamResult BindSharedParam(DocRvt doc, Category cat, string paramName, string grpName,
                                                            ParameterType paramType, bool visible, bool instanceBinding)
        {
            try // generic
            {
                AppRvt app = doc.Application;

                // This is needed already here to store old ones for re-inserting
                CategorySet catSet = app.Create.NewCategorySet();

                // Loop all Binding Definitions
                // IMPORTANT NOTE: Categories.Size is ALWAYS 1 !? For multiple categories, there is really one pair per each
                //                 category, even though the Definitions are the same...
                DefinitionBindingMapIterator iter = doc.ParameterBindings.ForwardIterator();
                while (iter.MoveNext())
                {
                    Definition def = iter.Key;
                    ElementBinding elemBind = (ElementBinding)iter.Current;

                    // Got param name match
                    if (paramName.Equals(def.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // Check for category match - Size is always 1!
                        if (elemBind.Categories.Contains(cat))
                        {
                            // Check Param Type
                            if (paramType != def.ParameterType) return BindSharedParamResult.eWrongParamType;
                            // Check Binding Type
                            if (instanceBinding)
                            {
                                if (elemBind.GetType() != typeof(InstanceBinding)) return BindSharedParamResult.eWrongBindingType;
                            }
                            else
                            {
                                if (elemBind.GetType() != typeof(TypeBinding)) return BindSharedParamResult.eWrongBindingType;
                            }
                            // Check Visibility - cannot (not exposed)
                            // If here, everything is fine, ie already defined correctly
                            return BindSharedParamResult.eAlreadyBound;
                        }
                        // If here, no category match, hence must store "other" cats for re-inserting
                        else
                        {
                            foreach (Category catOld in elemBind.Categories) catSet.Insert(catOld); //1 only, but no index...
                        }
                    }
                }

                // If here, there is no Binding Definition for it, so make sure Param defined and then bind it!
                DefinitionFile defFile = GetOrCreateSharedParamsFile(app);
                DefinitionGroup defGrp = GetOrCreateSharedParamsGroup(defFile, grpName);
                Definition definition = GetOrCreateSharedParamDefinition(defGrp, paramType, paramName, visible);
                catSet.Insert(cat);
                BindingRvt bind = null;
                if (instanceBinding)
                {
                    bind = app.Create.NewInstanceBinding(catSet);
                }
                else
                {
                    bind = app.Create.NewTypeBinding(catSet);
                }

                // There is another strange API "feature". If param has EVER been bound in a project (in above iter pairs or even if not there but once deleted), .Insert always fails!? Must use .ReInsert in that case.
                // See also similar findings on this topic in: http://thebuildingcoder.typepad.com/blog/2009/09/adding-a-category-to-a-parameter-binding.html - the code-idiom below may be more generic:
                if (doc.ParameterBindings.Insert(definition, bind))
                {
                    return BindSharedParamResult.eSuccessfullyBound;
                }
                else
                {
                    if (doc.ParameterBindings.ReInsert(definition, bind))
                    {
                        return BindSharedParamResult.eSuccessfullyBound;
                    }
                    else
                    {
                        return BindSharedParamResult.eFailed;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error in Binding Shared Param: {0}", ex.Message));
                return BindSharedParamResult.eFailed;
            }

            //return BindSharedParamResult.eSuccessfullyBound;
        }



    }
}
