#region Namespaces
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.IO;
#endregion

namespace FindImports
{

   // a generic interface to report imported data found in a specific project. 
   interface IReportImportData
   {
      bool init(string projectName);
      void startReportSection(string sectionName);
      void logItem(string item);
      void setWarning();
      void done();
      string getLogFileName();
   }

   class SimpleTextFileBasedReporter: IReportImportData
   {
      public SimpleTextFileBasedReporter()
      {
         
      }

      public bool init(string projectFileName) 
      {
         bool outcome = false;
         m_currentSection = null;
         m_warnUser = false;

         if (0 != projectFileName.Length)
         {
            m_projectFileName = projectFileName;
         }
         else
         {
            m_projectFileName = "Default";
         }

         m_logFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(m_projectFileName), System.IO.Path.GetFileNameWithoutExtension(m_projectFileName)) + "-ListOfImportedData.txt";

         // construct log file name from projectFileName and try to open file. Project file name is assumed to be valid (expected to be called on an open doc)
         try
         {
            m_outputFile = new StreamWriter(m_logFileName);
            m_outputFile.WriteLine("List of imported CAD data in " + projectFileName);
            outcome = true;
         }
         catch (System.UnauthorizedAccessException)
         {
            TaskDialog.Show("FindImports", "You are not authorized to create " + m_logFileName);
         }
         catch (System.ArgumentNullException) // oh, come on.
         {
            TaskDialog.Show("FindImports", "That's just not fair. Null argument for StreamWriter()");
         }
         catch (System.ArgumentException)
         {
            TaskDialog.Show("FindImports", "Failed to create " + m_logFileName);
         }
         catch (System.IO.DirectoryNotFoundException)
         {
            TaskDialog.Show("FindImports", "That's not supposed to happen: directory not found: " + System.IO.Path.GetDirectoryName(m_projectFileName));
         }
         catch (System.IO.PathTooLongException)
         {
            TaskDialog.Show("FindImports", "The OS thinks the file name " + m_logFileName + "is too long");
         }
         catch (System.IO.IOException)
         {
            TaskDialog.Show("FindImports", "An IO error has occurred while writing to " + m_logFileName);
         }
         catch (System.Security.SecurityException)
         {
            TaskDialog.Show("FindImports", "The OS thinks your access rights to" + System.IO.Path.GetDirectoryName(m_projectFileName) + "are insufficient");
         }

         return outcome;
      }

      public void startReportSection(string sectionName)
      {
         endReportSection();
         m_outputFile.WriteLine();
         m_outputFile.WriteLine(sectionName);
         m_outputFile.WriteLine();

         m_currentSection = sectionName;
      }


      public void logItem(string item)
      {
         m_outputFile.WriteLine(item);
      }

      public void setWarning()
      {
         m_warnUser = true;
      }

      public void done()
      {
         endReportSection();
         m_outputFile.WriteLine();
         m_outputFile.WriteLine("The End");
         m_outputFile.WriteLine();
         m_outputFile.Close();

         // display "done" dialog, potentially open log file
         TaskDialog doneMsg = null;
         new TaskDialog("FindImports completed successfully");
         if (m_warnUser)
         {
            doneMsg = new TaskDialog("Potential issues found. Please review the log file");
         }
         else
         {
            doneMsg = new TaskDialog("FindImports completed successfully");
         }

         doneMsg.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "Review " + m_logFileName);
         switch (doneMsg.Show())
         {
            default:
               break;
            case TaskDialogResult.CommandLink1:
               // display the log file
               Process.Start("notepad.exe", m_logFileName);
               break;
         }

      }

      public string getLogFileName()
      {
         return m_logFileName;
      }

      private void endReportSection()
      {
         if (null != m_currentSection)
         {
            m_outputFile.WriteLine();
            m_outputFile.WriteLine("End of " + m_currentSection);
            m_outputFile.WriteLine();
         }
      }

      private string m_projectFileName;
      private string m_logFileName;
      private StreamWriter m_outputFile;
      private string m_currentSection;
      private bool m_warnUser; // tell the user to review the log file

   }

   [Transaction(TransactionMode.Manual)]
   public class Command : IExternalCommand
   {
      public Result Execute(
        ExternalCommandData commandData,
        ref string message,
        ElementSet elements)
      {
         UIApplication uiapp = commandData.Application;
         UIDocument uidoc = uiapp.ActiveUIDocument;
         Application app = uiapp.Application;
         Document doc = uidoc.Document;
         listImports(doc);
         return Result.Succeeded;
      }

      private void listImports(Document doc)
      {
         FilteredElementCollector col = new FilteredElementCollector(doc).OfClass(typeof(ImportInstance));
         NameValueCollection listOfViewSpecificImports = new NameValueCollection();
         NameValueCollection listOfModelImports = new NameValueCollection();
         NameValueCollection listOfUnidentifiedImports = new NameValueCollection();

         foreach (Element e in col)
         {
            // collect all view-specific names
            if (e.ViewSpecific)
            {
               string viewName = null;

               try
               {
                  Element viewElement = doc.GetElement(e.OwnerViewId);
                  viewName = viewElement.Name;
               }
               catch (Autodesk.Revit.Exceptions.ArgumentNullException) // just in case
               {
               	viewName = String.Concat("Invalid View ID: ", e.OwnerViewId.ToString());
               }


               if (null != e.Category)
               {
                  listOfViewSpecificImports.Add(importCategoryNameToFileName(e.Category.Name), viewName);
               }
               else
               {
                  listOfUnidentifiedImports.Add(e.Id.ToString(), viewName);
               }
            }
            else
            {
               listOfModelImports.Add(importCategoryNameToFileName(e.Category.Name), e.Name);
            }
         }

         IReportImportData logOutput = new SimpleTextFileBasedReporter();
         if (!logOutput.init(doc.PathName))
         {
            TaskDialog.Show("FindImports", "Unable to create report file");
         }
         else
         {
            if (listOfViewSpecificImports.HasKeys())
            {
               logOutput.startReportSection("View Specific Imports");

               listResults(listOfViewSpecificImports, logOutput);
            }

            if (listOfModelImports.HasKeys())
            {
               logOutput.startReportSection("Model Imports");
               listResults(listOfModelImports, logOutput);
            }

            if (listOfUnidentifiedImports.HasKeys())
            {
               logOutput.startReportSection("Unknown import instances");
               listResults(listOfUnidentifiedImports, logOutput);
            }

            if (!sanityCheckViewSpecific(listOfViewSpecificImports, logOutput))
            {
               logOutput.setWarning();
               //TaskDialog.Show("FindImportedData", "Possible issues found. Please review the log file");
            }
            
            logOutput.done();
         }
      }

      // this is an import category. It is created from a CAD file name, with appropriate (number) added.
      // we want to use the file name as a key for our list of import instances, so strip off the brackets
      private string importCategoryNameToFileName(string catName)
      {
         string fileName = catName;
         fileName = fileName.Trim();
         
         if (fileName.EndsWith(")"))
         {
            int lastLeftBracket = fileName.LastIndexOf("(");
            
            if (-1 != lastLeftBracket)
               fileName = fileName.Remove(lastLeftBracket); // remove left bracket
         }
         
         return fileName.Trim();
      }

      private void listResults(NameValueCollection listOfImports, IReportImportData logFile)
      {
         
         foreach (String key in listOfImports.AllKeys)
         {
           logFile.logItem(key + ": " + listOfImports.Get(key));
         }
      }

      // ran a few basic sanity checks on the list of view-specific imports. View-specific sanity is not the same as model sanity. Neither is necessarily sane.
      // true means possibly sane, false means probably not
      private bool sanityCheckViewSpecific(NameValueCollection listOfImports, IReportImportData logFile)
      {
         logFile.startReportSection("Sanity check report for view-specific imports");
         
         bool status = true;
         // count number of entities per key.
         foreach (String key in listOfImports.AllKeys)
         {
            string[] levels = listOfImports.GetValues(key);
            if (levels != null && levels.GetLength(0) > 1)
            {
               logFile.logItem("CAD data " + key + " appears to have been imported in Current View Only mode multiple times. It is present in views "+ listOfImports.Get(key));
               status = false;
            }
         }
         
         return status;
      }
   }
}
