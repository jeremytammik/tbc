
# Get the line endpoint that has the lowest Z value.
def GetLowerZEndPoint(line):
  return line.get_EndPoint(0 if line.Direction.Z > 0 else 1)

# Determine if the line is vertical (parallel to the Z axis).
def IsVerticalLine(line):
  return line.Direction.CrossProduct(XYZ.BasisZ).IsAlmostEqualTo(XYZ.Zero)

# Determine if the vector represented by an XYZ value is oriented vertically up (parallel to the Z axis).
def IsUpVector(xyz):
  return xyz.Normalize().IsAlmostEqualTo(XYZ.BasisZ)

# Get a list of lines representing the scope box geometry.
def GetScopeBoxLines(scopeBox):
  return list(scopeBox.get_Geometry(Options()))

# Given a line and one of its end points, return the other end point.
def GetOppositeEndPoint(line, endPoint):
  ep1 = line.get_EndPoint(0)
  ep2 = line.get_EndPoint(1)
  return ep1 if ep2.IsAlmostEqualTo(endPoint) else ep2 if ep1.IsAlmostEqualTo(endPoint) else None

# Given an origin and three vectors representing the direction and lengths of three dimensions,
# return a bounding box with an appropriate transform, min and max values.
def GetBoundingBoxXYZ(origin, v_x, v_y, v_z):
  t = Transform.Identity
  t.Origin = origin
  t.BasisX = v_x.Normalize()
  t.BasisY = v_y.Normalize()
  t.BasisZ = v_z.Normalize()
  bbox = BoundingBoxXYZ()
  bbox.Transform = t
  bbox.Min = XYZ.Zero
  bbox.Max = XYZ(v_x.GetLength(), v_y.GetLength(), v_z.GetLength())
  return bbox

# Given a scope box element, return a bounding box matching the scope box geometry.
def GetScopeBoxBoundingBoxXYZ(scopeBox):
  lines = GetScopeBoxLines(scopeBox)
  # Choose an appropriate origin point.
  verticalLines = list(l for l in lines if IsVerticalLine(l))
  origin = GetLowerZEndPoint(verticalLines[0])
  # Compute a list of vectors representing the length and orientation of scope box lines emanating
  # from the chosen origin point. These vectors represent the three dimensions of the scope box.
  originVectors = list(p - origin for p in (GetOppositeEndPoint(l, origin) for l in lines) if p is not None)
  # Choose the vector that points up from the origin. This vector serves as the Z dimension of the bounding box.
  v_z = list(v for v in originVectors if IsUpVector(v))[0]
  # Choose the other two vectors representing the X and Y dimensions of the bounding box.
  v1, v2 = list(v for v in originVectors if not v.IsAlmostEqualTo(v_z))
  # Which vector is the X dimension and which is the Y dimension depends on their cross product.
  # The three dimension vectors must form a right handed coordinate system.
  v_x, v_y = (v1, v2) if v1.CrossProduct(v2).Normalize().IsAlmostEqualTo(v_z.Normalize()) else (v2, v1)
  # Construct a bounding box representing the scope box geometry.
  return GetBoundingBoxXYZ(origin, v_x, v_y, v_z)

# Set 3D view's section box to match the specified scope box element extents.
def Test(scopeBox, view3d):
  bbox = GetScopeBoxBoundingBoxXYZ(scopeBox)
  tranny = Transaction(doc, "set view section box to scope box extents")
  tranny.Start()
  view3d.SectionBox = bbox
  tranny.Commit()
  return tranny.GetStatus()

# Before running the following sample code, ensure the active view is a 3D view,
# and ensure that you've selected exactly one scope box in the view.

#scopeBox = selection[0]
#view3d = doc.ActiveView
#Test(scopeBox, view3d)
