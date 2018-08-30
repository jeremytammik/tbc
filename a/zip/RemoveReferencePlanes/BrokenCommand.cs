using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;


namespace RemoveReferencePlanes
{
    [TransactionAttribute(TransactionMode.Manual)]

    public class BrokenCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            // There is likely an easier way to do this using an exclusion filter but
            // this being my first foray into filtering with Revit, I couldn't get that working
            FilteredElementCollector filt = new FilteredElementCollector(doc);
            List<ElementId> refIDs = filt.OfClass(typeof(ReferencePlane)).ToElementIds().ToList();

            using (TransactionGroup tg = new TransactionGroup(doc))
            {
                tg.Start("Remove Un-Used Reference Planes");
                foreach (ElementId id in refIDs)
                {
                    var filt2 = new ElementClassFilter(typeof(FamilyInstance));

                    var filt3 = new ElementParameterFilter(new FilterElementIdRule(new ParameterValueProvider(new ElementId(BuiltInParameter.HOST_ID_PARAM)), new FilterNumericEquals(), id));
                    var filt4 = new LogicalAndFilter(filt2, filt3);

                    var thing = new FilteredElementCollector(doc);

                    using (Transaction t = new Transaction(doc))
                    {
                        // Check for hosted elements on the plane
                        if (thing.WherePasses(filt4).Count() == 0)
                        {
                            t.Start("Do The Thing");

#if Revit2018
                                        if (doc.GetElement(id).GetDependentElements(new ElementClassFilter(typeof(FamilyInstance))).Count == 0)
                                        {
                                            doc.Delete(id);
                                        }

                                        t.Commit();
#else

                            // Make sure there is nothing measuring to the plane
                            if (doc.Delete(id).Count() > 1)
                            {
                                t.Dispose();
                                // Skipped
                            }
                            else
                            {
                                // Deleted
                                t.Commit();
                            }
#endif
                        }
                        else
                        {
                            // Skipped
                        }
                    }
                }
                tg.Assimilate();
            }
            return Result.Succeeded;
        }
    }
}
