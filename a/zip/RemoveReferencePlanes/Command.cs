using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace RemoveReferencePlanes
{
    [TransactionAttribute(TransactionMode.Manual)]
public class Command : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {

        Document doc = commandData.Application.ActiveUIDocument.Document;

        FilteredElementCollector filt = new FilteredElementCollector(doc);
        List<ElementId> refIDs = filt.OfClass(typeof(ReferencePlane)).ToElementIds().ToList();

        using (TransactionGroup tg = new TransactionGroup(doc, "Remove un-used reference planes"))
        {
            tg.Start();

            FilteredElementCollector elementFilter = new FilteredElementCollector(doc);

            List<Element> elems = filt.OfClass(typeof(FamilyInstance)).ToList();

            List<ElementId> toKeep = new List<ElementId>();

            foreach (Element elem in elems)
            {
                // Make sure the element is hosted
                if ((elem as FamilyInstance).Host != null)
                {
                    ElementId hostId = ((FamilyInstance)elem).Host.Id;

                    // Check list to see if we've already added this plane
                    if (!toKeep.Contains(hostId))
                    {
                        toKeep.Add(hostId);
                    }
                }
            }

            // Loop through reference planes and delete the ones not in the list toKeep
            foreach (ElementId refid in refIDs)
            {
                using (Transaction t = new Transaction(doc, "Removing plane " + doc.GetElement(refid).Name))
                {
                    if (!toKeep.Contains(refid))
                    {
                        t.Start();

                        // Make sure there are no dimensions measuring to the plane
                        if (doc.Delete(refid).Count > 1)
                        {
                            t.Dispose();
                        }
                        else
                        {
                            t.Commit();
                        }
                    }
                }
            }
            tg.Assimilate();
        }
        return Result.Succeeded;
    }
}
}
