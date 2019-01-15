import clr
import math
clr.AddReference('RevitAPI')
clr.AddReference('RevitAPIUI')
from Autodesk.Revit.DB import *
from Autodesk.Revit.DB.Architecture import *
from Autodesk.Revit.DB.Analysis import *
uidoc = __revit__.ActiveUIDocument
doc = __revit__.ActiveUIDocument.Document
app = doc.Application
docs = app.Documents

n = docs.Size

print n, 'open documents:'

for d in docs:
  s = d.PathName
  print s
  if s.endswith('.ifc.RVT'): ifcdoc = d

print 'Linked-in IFC document:'
print ifcdoc.PathName

collector = FilteredElementCollector(ifcdoc).OfClass(clr.GetClrType(DirectShape)).OfCategory(BuiltInCategory.OST_GenericModel)

print collector.GetElementCount(), 'generic model direct shape elements'

def get_param(e,s):
  "Return string parameter value for given parameter name"
  ps = e.GetParameters(s)
  n = ps.Count
  assert(2 > n)
  if 0 < n: return ps[0].AsString()
  else: return None
  
def is_zone(e):
  "Predicate returning True is e is an IfcZone"
  export_as = get_param(e,'IfcExportAs')
  return export_as and export_as == 'IfcZone'

def zone_name(e):
  "Return IfcName of IfcZone element or None"
  if is_zone(e):
    return get_param(e,'IfcName')

zone_names = []

for e in collector:
  if is_zone(e):
    zone_names.append(get_param(e,'IfcName'))

zone_names.sort()

n = len(zone_names)

print n, 'zones:', zone_names
