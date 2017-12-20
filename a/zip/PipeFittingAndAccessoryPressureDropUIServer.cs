using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Plumbing;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExternalService;
using Autodesk.Revit.DB.ExtensibleStorage;
using FittingAndAccessoryCalculationManaged;
using FittingAndAccessoryCalculationServers.Pipe;
using FittingAndAccessoryCalculationUIServers.Duct;
using Autodesk.Revit.DB.Plumbing;

namespace FittingAndAccessoryCalculationUIServers.Pipe
{
   public class NotDefinedPressureDropUIServer : IPipeFittingAndAccessoryPressureDropUIServer
   {
      /// <summary>
      /// Returns the id of the corresponding DB server for which this server provides an optional UI. 
      /// </summary>
      public System.Guid GetDBServerId()
      {
         return new Guid("61E7B8E1-16D1-4FE4-82F0-327AF736323F");
      }

      /// <summary>
      /// Prompts the setting UI for the user. 
      /// This method might be invoked only when the server has UI settings (HasSettings == True).
      /// </summary>
      /// <param name="data">
      /// The pipe fitting and accessory pressure drop UI data.
      /// It is used as in/out param, the user can get the old values from it and also can set the new values from the setting UI back to it.
      /// </param>
      /// <returns>
      /// True if the user does change something in the UI (i.e. the user changes something in the entity in the data that was given as the argument into this method.), False otherwise.
      /// </returns>
      public bool ShowSettings(PipeFittingAndAccessoryPressureDropUIData data)
      {
         return false;
      }

      /// <summary>
      /// Returns the Id of the server. 
      /// </summary>
      public System.Guid GetServerId()
      {
         return new Guid("62917391-E14D-4897-AD0C-05342D71E4F9");
      }

      /// <summary>
      /// Returns the Id of the service that the sever belongs to. 
      /// </summary>
      public ExternalServiceId GetServiceId()
      {
         return ExternalServices.BuiltInExternalServices.PipeFittingAndAccessoryPressureDropUIService;
      }

      /// <summary>
      /// Returns the server's name. 
      /// </summary>
      public System.String GetName()
      {
         return Properties.Resources.PipeNotDefinedPressureDropUIServerName;
      }

      /// <summary>
      /// Returns the server's vendor Id. 
      /// </summary>
      public System.String GetVendorId()
      {
         return "ADSK";
      }

      /// <summary>
      /// Returns the description of the server. 
      /// </summary>
      public System.String GetDescription()
      {
         return Properties.Resources.PipeNotDefinedPressureDropUIServerDescription;
      }
   }

   public class SpecificCoefficientPressureDropUIServer : IPipeFittingAndAccessoryPressureDropUIServer
   {
      private static List<string> coefficientHistory = new List<string>();

      /// <summary>
      /// Returns the id of the corresponding DB server for which this server provides an optional UI. 
      /// </summary>
      public System.Guid GetDBServerId()
      {
         return new Guid("32D58662-B467-4F7B-B728-F6AD7B7BA5E3");
      }

      /// <summary>
      /// Prompts the setting UI for the user. 
      /// This method might be invoked only when the server has UI settings (HasSettings == True).
      /// </summary>
      /// <param name="data">
      /// The pipe fitting and accessory pressure drop UI data.
      /// It is used as in/out param, the user can get the old values from it and also can set the new values from the setting UI back to it.
      /// </param>
      /// <returns>
      /// True if the user does change something in the UI (i.e. the user changes something in the entity in the data that was given as the argument into this method.), False otherwise.
      /// </returns>
      public bool ShowSettings(PipeFittingAndAccessoryPressureDropUIData data)
      {
         bool settingChanged = false;

         // Gets initial value for settings dialog.
         Guid dbServerId = GetDBServerId();
         string initialCoefficient = CalculationUtility.GetInitialValue(data, PipeSchemaBuildingUtility.fieldKFactor);

         SpecificCoefficientSettingsDlg settingsDlg = new SpecificCoefficientSettingsDlg(CalculationUtility.GetInitialItemsForComboBox(coefficientHistory, initialCoefficient), ConnectorDomainType.Piping, GetDBServerId());

         if (settingsDlg.ShowDialog() == DialogResult.OK)
         {
            string coefficient = settingsDlg.Coefficient;
            settingChanged = CalculationUtility.UpdateEntities(data, dbServerId, PipeSchemaBuildingUtility.fieldKFactor, coefficient);
            CalculationUtility.UpdateHistory(coefficientHistory, coefficient);
         }

         return settingChanged;
      }

      /// <summary>
      /// Returns the Id of the server. 
      /// </summary>
      public System.Guid GetServerId()
      {
         return new Guid("247265B6-7B71-4147-97C6-CC52B39C12AA");
      }

      /// <summary>
      /// Returns the Id of the service that the sever belongs to. 
      /// </summary>
      public ExternalServiceId GetServiceId()
      {
         return ExternalServices.BuiltInExternalServices.PipeFittingAndAccessoryPressureDropUIService;
      }

      /// <summary>
      /// Returns the server's name. 
      /// </summary>
      public System.String GetName()
      {
         return Properties.Resources.PipeSpecificCoefficientPressureDropUIServerName;
      }

      /// <summary>
      /// Returns the server's vendor Id. 
      /// </summary>
      public System.String GetVendorId()
      {
         return "ADSK";
      }

      /// <summary>
      /// Returns the description of the server. 
      /// </summary>
      public System.String GetDescription()
      {
         return Properties.Resources.PipeSpecificCoefficientPressureDropUIServerDescription;
      }
   }

   public class SpecificLossPressureDropUIServer : IPipeFittingAndAccessoryPressureDropUIServer
   {
      private static List<double> pressureLossHistory = new List<double>();

      /// <summary>
      /// Returns the id of the corresponding DB server for which this server provides an optional UI. 
      /// </summary>
      public System.Guid GetDBServerId()
      {
         return new Guid("16F4F7BE-0AC0-461D-A9A4-1D3511CD280E");
      }

      /// <summary>
      /// Prompts the setting UI for the user. 
      /// This method might be invoked only when the server has UI settings (HasSettings == True).
      /// </summary>
      /// <param name="data">
      /// The pipe fitting and accessory pressure drop UI data.
      /// It is used as in/out param, the user can get the old values from it and also can set the new values from the setting UI back to it.
      /// </param>
      /// <returns>
      /// True if the user does change something in the UI (i.e. the user changes something in the entity in the data that was given as the argument into this method.), False otherwise.
      /// </returns>
      public bool ShowSettings(PipeFittingAndAccessoryPressureDropUIData data)
      {
         bool settingChanged = false;

         // Gets initial value for settings dialog.
         Guid dbServerId = GetDBServerId();
         double initialPressureLoss = CalculationUtility.GetInitialNumericValue(data, PipeSchemaBuildingUtility.fieldPressureLoss);

         SpecificLossSettingsDlg settingsDlg = new SpecificLossSettingsDlg(CalculationUtility.GetInitialItemsForComboBox(pressureLossHistory, initialPressureLoss, data.GetUnits(), UnitType.UT_Piping_Pressure), data.GetUnits(), UnitType.UT_Piping_Pressure, ConnectorDomainType.Piping, GetDBServerId());

         if (settingsDlg.ShowDialog() == DialogResult.OK)
         {
            settingChanged = CalculationUtility.UpdateEntities(data, dbServerId, PipeSchemaBuildingUtility.fieldPressureLoss, settingsDlg.PressureLoss.ToString());
            CalculationUtility.UpdateHistory(pressureLossHistory, settingsDlg.PressureLoss);
         }

         return settingChanged;
      }

      /// <summary>
      /// Returns the Id of the server. 
      /// </summary>
      public System.Guid GetServerId()
      {
         return new Guid("5978B92A-3F0B-4691-BBC8-CAF2FAE20A71");
      }

      /// <summary>
      /// Returns the Id of the service that the sever belongs to. 
      /// </summary>
      public ExternalServiceId GetServiceId()
      {
         return ExternalServices.BuiltInExternalServices.PipeFittingAndAccessoryPressureDropUIService;
      }

      /// <summary>
      /// Returns the server's name. 
      /// </summary>
      public System.String GetName()
      {
         return Properties.Resources.PipeSpecificLossPressureDropUIServerName;
      }

      /// <summary>
      /// Returns the server's vendor Id. 
      /// </summary>
      public System.String GetVendorId()
      {
         return "ADSK";
      }

      /// <summary>
      /// Returns the description of the server. 
      /// </summary>
      public System.String GetDescription()
      {
         return Properties.Resources.PipeSpecificLossPressureDropUIServerDescription;
      }
   }

   public class KFactorTablePipePressureDropUIServer : IPipeFittingAndAccessoryPressureDropUIServer
   {

      /// <summary>
      /// Returns the id of the corresponding DB server for which this server provides an optional UI. 
      /// </summary>
      public System.Guid GetDBServerId()
      {
         return new Guid("51DD5E98-A9DD-464B-B286-4A37953610BF");
      }

      /// <summary>
      /// Prompts the setting UI for the user. 
      /// This method might be invoked only when the server has UI settings (HasSettings == True).
      /// </summary>
      /// <param name="data">
      /// The pipe fitting and accessory pressure drop UI data.
      /// It is used as in/out param, the user can get the old values from it and also can set the new values from the setting UI back to it.
      /// </param>
      /// <returns>
      /// True if the user does change something in the UI (i.e. the user changes something in the entity in the data that was given as the argument into this method.), False otherwise.
      /// </returns>
      public bool ShowSettings(PipeFittingAndAccessoryPressureDropUIData data)
      {
         bool settingChanged = false;


         // Gets initial value for settings dialog.
         Guid dbServerId = GetDBServerId();
         string initialTableName = CalculationUtility.GetInitialValue(data, PipeSchemaBuildingUtility.fieldKFactorableName);

         List<string> validTables = new List<string>();
         IList<PipeFittingAndAccessoryPressureDropUIDataItem> uiDataItems = data.GetUIDataItems();

         if (uiDataItems.Count > 0)
         {
            // If all fittings or accessories have the same PipeKFactorPartType, we will list all tables of that PipeKFactorPartType as initial table list; otherwise the initial table list is empty.
            if (CalculationUtility.HasSamePipeKFactorPartType(uiDataItems))
            {
               PipeFittingAndAccessoryData fittingData = uiDataItems[0].GetPipeFittingAndAccessoryData();
               KFactorTablePipePressureDropCalculator.getValidTableNames(fittingData, validTables);
            }
         }

         if (string.IsNullOrEmpty(initialTableName) && validTables.Count > 0 && uiDataItems.Count == 1)
            initialTableName = validTables[0];

         SpecificTableNameSettingsDlg settingDlg = new SpecificTableNameSettingsDlg(initialTableName, validTables, ConnectorDomainType.Piping, GetDBServerId());

         
         if (settingDlg.ShowDialog() == DialogResult.OK)
         {
            string tableName = settingDlg.TableName;
            settingChanged = CalculationUtility.UpdateEntities(data, dbServerId, PipeSchemaBuildingUtility.fieldKFactorableName, tableName);
         }

         return settingChanged;
      }

      /// <summary>
      /// Returns the Id of the server. 
      /// </summary>
      public System.Guid GetServerId()
      {
         return new Guid("CDA19B6F-FBD5-4725-A0CE-F159BF5D02FE");
      }

      /// <summary>
      /// Returns the Id of the service that the sever belongs to. 
      /// </summary>
      public ExternalServiceId GetServiceId()
      {
         return ExternalServices.BuiltInExternalServices.PipeFittingAndAccessoryPressureDropUIService;
      }

      /// <summary>
      /// Returns the server's name. 
      /// </summary>
      public System.String GetName()
      {
         return Properties.Resources.PipeKFactorFromTablePressureDropUIServerName;
      }

      /// <summary>
      /// Returns the server's vendor Id. 
      /// </summary>
      public System.String GetVendorId()
      {
         return "ADSK";
      }

      /// <summary>
      /// Returns the description of the server. 
      /// </summary>
      public System.String GetDescription()
      {
         return Properties.Resources.PipeKFactorFromTablePressureDropUIServerDescription;
      }
   }
}
