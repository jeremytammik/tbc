using System;
using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace FailureProcessor
{
   /// <summary>
   /// While it is active, this FailuresProcessor ignores all warnings which occur.
   /// For failures which are errors, the FailureProcessor will attempt to resolve it using the default resolution.
   /// If the default resolution is to delete any element(s) the active transaction is rolled back.
   /// Detailed documentation is here:
   /// http://help.autodesk.com/view/RVT/2018/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Advanced_Topics_Failure_Posting_and_Handling_Handling_Failures_html 
   /// </summary>
   public class FailureProcessor : IFailuresProcessor
   {
      public void Dismiss(Document doc)
      {}

      const int MAX_RESOLUTION_ATTEMPTS = 3;

      public FailureProcessingResult ProcessFailures(FailuresAccessor failuresAccessor)
      {
         IList<FailureMessageAccessor> failList = new List<FailureMessageAccessor>();
         failList = failuresAccessor.GetFailureMessages();
         int errorCount = 0;
         bool hasError = false;
         bool hasWarning = false;
         IList<FailureResolutionType> resolutionTypeList = new List<FailureResolutionType>();

         foreach (FailureMessageAccessor failure in failList)
         {
            // Check how many resolution types were attempted to try to prevent
            // the application from entering an infinite loop.
            resolutionTypeList = failuresAccessor.GetAttemptedResolutionTypes(failure);
            
            if (resolutionTypeList.Count >= MAX_RESOLUTION_ATTEMPTS)
            {
               Console.WriteLine("Failure: Attempted to resolve the failure " 
                  + failure.GetDescriptionText() + " " + resolutionTypeList.Count 
                  + " times with resolution " + failure.GetCurrentResolutionType()
                  + ". Rolling back transaction.");
               return FailureProcessingResult.ProceedWithRollBack;
            }

            // If the default resolution for the error results in deleting the elements then
            // just skip and proceed with rollback.
            if (failure.GetDefaultResolutionCaption().Equals("Delete Element(s)"))
            {
               Console.WriteLine("Failure: Unable to continue because of posted errors. Rolling back transaction.");
               return FailureProcessingResult.ProceedWithRollBack;
            }

            if (failure.GetSeverity() == FailureSeverity.Error && failure.GetFailingElementIds().Count > 0)
            {
               hasError = true;
               ++errorCount;
               failuresAccessor.ResolveFailure(failure);
            }

            if (failure.GetSeverity() == FailureSeverity.Warning)
            {
               hasWarning = true;
               failuresAccessor.DeleteWarning(failure);
            }

            // If an attempt to resolve failures are made then return the result with ProceedWithCommit
            // Errors are not removed by resolveErrors - only subsequent regeneration will actually remove them.
            // The removal may also fail - resolution is not guaranteed to succeed. So returning with 
            // FailureProcessingResult.ProceedWithCommit is required
            if (hasWarning || hasError)
            {
               return FailureProcessingResult.ProceedWithCommit;
            }

            Console.WriteLine("Failure " + errorCount + ": " + " Severity: " + failure.GetSeverity() + " " + failure.GetDescriptionText());
         }

         // Default: try continuing.
         Console.WriteLine("Attempting to continue.");
         return FailureProcessingResult.Continue;
      }
   }
}
