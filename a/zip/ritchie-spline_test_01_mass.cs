using System;
using System.Collections.Generic;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;

// Spline + Derivatives Test
// Ritchie Jackson : November 2010

namespace AAC_Thesis
{
    class Spline_Test_01_Mass
    {
        // Conversion Revit Default Imperial 'foot' to Metric 'millimeter'
        static double ftMM = 1 / 304.8;

        // Constructor
        public Spline_Test_01_Mass(Document doc)
        {
            // Spline Points and associated Reference Points
            ReferencePointArray splinePntsR = new ReferencePointArray();
            ReferencePoint rp;
            XYZ pnt1 = new XYZ();
            rp = doc.FamilyCreate.NewReferencePoint(pnt1);
            splinePntsR.Append(rp);
            XYZ pnt2 = new XYZ(0.0, 0.0, 1500.0) * ftMM;
            rp = doc.FamilyCreate.NewReferencePoint(pnt2);
            splinePntsR.Append(rp);
            XYZ pnt3 = new XYZ(2700.0, 3600.0, 3600.0) * ftMM;
            rp = doc.FamilyCreate.NewReferencePoint(pnt3);
            splinePntsR.Append(rp);
            XYZ pnt4 = new XYZ(1200.0, -900.0, 4800.0) * ftMM;
            rp = doc.FamilyCreate.NewReferencePoint(pnt4);
            splinePntsR.Append(rp);
            XYZ pnt5 = new XYZ(300.0, 300.0, 7200.0) * ftMM;
            rp = doc.FamilyCreate.NewReferencePoint(pnt5);
            splinePntsR.Append(rp);
            XYZ pnt6 = new XYZ(0.0, 0.0, 9600.0) * ftMM;
            rp = doc.FamilyCreate.NewReferencePoint(pnt6);
            splinePntsR.Append(rp);

            // Create Curve
            CurveByPoints splineF = doc.FamilyCreate.NewCurveByPoints(splinePntsR);
            // Get Moving Frame of Curve by Points
            showDeriv(doc, splineF, 0.75, "Curve by Points");

            // Hermite Spline Array
            IList<XYZ> splinePnts = new List<XYZ>();
            splinePnts.Add(pnt1);
            splinePnts.Add(pnt2);
            splinePnts.Add(pnt3);
            splinePnts.Add(pnt4);
            splinePnts.Add(pnt5);
            splinePnts.Add(pnt6);
            // Create Hermite Spline
            HermiteSpline spline = doc.Application.Create.NewHermiteSpline(splinePnts, false);
            // Get Moving Frame of Hermite Spline
            showDeriv(doc, spline, 0.75, "Hermite Spline");

            // Arc through first 3 Points of Spline
            Arc arc = doc.Application.Create.NewArc(pnt1, pnt3, pnt2);
            Line arcL = doc.Application.Create.NewLine(pnt1, pnt3, true);
            CurveArray planeCurve = new CurveArray();
            planeCurve.Append(arc);
            planeCurve.Append(arcL);
            Plane plane = doc.Application.Create.NewPlane(planeCurve);
            SketchPlane planeSK = doc.FamilyCreate.NewSketchPlane(plane);
            ModelArc arcM = doc.FamilyCreate.NewModelCurve(arc, planeSK) as ModelArc;
            // Get Moving Frame of Arc
            showDeriv(doc, arc, 0.75, "Arc");
        }
        // Get Derivatives at Point on CurveByPoints
        private void showDeriv(Document doc, CurveByPoints curve, double param, String type)
        {
            // Convert 'CurveByPoints' to enable 'Derivative' functionality
            Curve geomE = curve.GeometryCurve;
            Transform deriv = geomE.ComputeDerivatives(param, true);
            XYZ paramPnt = geomE.Evaluate(param, true);
            // Display the Curve and Moving Frame
            showInfo(doc, deriv, type, param, paramPnt);
        }
        // Get Derivatives at Point on Curve - Hermite Spline / Arc
        private void showDeriv(Document doc, Curve curve, double param, String type)
        {
            Transform deriv = curve.ComputeDerivatives(param, true);
            XYZ paramPnt = curve.Evaluate(param, true);
            // Display the Curve and Moving Frame
            showInfo(doc, deriv, type, param, paramPnt);
        }
        // Display the Curve and Moving Frame
        private void showInfo(Document doc, Transform deriv, String curveType,
            double param, XYZ paramPnt)
        {
            // Basis Scale Factor
            double scaleB = 900.0 * ftMM;
            // Display Basis Values and Dot-Products
            TaskDialog.Show("Debug", "Derivatives of " + curveType + " at " + param + ": " +
                "\n\tBasisX: " + Math.Round(deriv.BasisX.X, 3) + ", " +
                                 Math.Round(deriv.BasisX.Y, 3) + ", " +
                                 Math.Round(deriv.BasisX.Z, 3) + ", " +
                "\n\tBasisY: " + Math.Round(deriv.BasisY.X, 3) + ", " +
                                 Math.Round(deriv.BasisY.Y, 3) + ", " +
                                 Math.Round(deriv.BasisY.Z, 3) + ", " +
                "\n\tBasisZ: " + Math.Round(deriv.BasisZ.X, 3) + ", " +
                                 Math.Round(deriv.BasisZ.Y, 3) + ", " +
                                 Math.Round(deriv.BasisZ.Z, 3) +
                "\nDot-Products - Should be Zero:-" +
                "\n\tXY: " + Math.Round(deriv.BasisX.DotProduct(deriv.BasisY), 0) +
                "\n\tXZ: " + Math.Round(deriv.BasisX.DotProduct(deriv.BasisZ), 0) +
                "\n\tYZ: " + Math.Round(deriv.BasisY.DotProduct(deriv.BasisZ), 0));
            // End-Point of BasisX Indicator
            XYZ xBase = paramPnt.Add(deriv.BasisX.Normalize() * scaleB);
            // Support Vector for Sketch Plane creation
            XYZ xVect = new XYZ(deriv.BasisX.X, deriv.BasisX.Y, 0.0);
            // End-Point of 'false' BasisY Indicator
            XYZ yBase = paramPnt.Add(deriv.BasisY.Normalize() * scaleB);
            // Support Vector for Sketch Plane creation
            XYZ yVect = new XYZ(deriv.BasisY.X, deriv.BasisY.Y, 0.0);
            // End-Point of BasisZ Indicator
            XYZ zBase = paramPnt.Add(deriv.BasisZ.Normalize() * scaleB);
            // Support Vector for Sketch Plane creation
            XYZ zVect = new XYZ(deriv.BasisZ.X, deriv.BasisZ.Y, 0.0);

            // Resolve Problem with BasisY on Splines (Arc is O.K.)
            // Compute BasisY as the Cross-Product of BasisX & Z
            XYZ xzCros = deriv.BasisZ.CrossProduct(deriv.BasisX);
            // Calculate 'true' BasisY Vector
            xzCros = xzCros.Normalize() * scaleB;
            // End-Point of 'true' BasisY Indicator
            XYZ xzBase = paramPnt.Add(xzCros);
            // Support Vector for Sketch Plane creation
            XYZ xzVec = new XYZ(xzCros.X, xzCros.Y, 0.0);
            // Create Moving Frame Indicators
            Line xBaseL = doc.Application.Create.NewLine(paramPnt, xBase, true);
            Line yBaseL = doc.Application.Create.NewLine(paramPnt, yBase, true);
            Line zBaseL = doc.Application.Create.NewLine(paramPnt, zBase, true);
            Line xzBaseL = doc.Application.Create.NewLine(paramPnt, xzBase, true);
            // Display Moving Frame Indicators
            // BasisX
            Plane xBaseP = doc.Application.Create.NewPlane(xVect, XYZ.BasisZ, paramPnt);
            SketchPlane xBasePSK = doc.FamilyCreate.NewSketchPlane(xBaseP);
            ModelLine xBaseLM = doc.FamilyCreate.NewModelCurve(xBaseL, xBasePSK) as ModelLine;
            // BasisY - 'false'
            Plane yBaseP = doc.Application.Create.NewPlane(yVect, XYZ.BasisZ, paramPnt);
            SketchPlane yBasePSK = doc.FamilyCreate.NewSketchPlane(yBaseP);
            ModelLine yBaseLM = doc.FamilyCreate.NewModelCurve(yBaseL, yBasePSK) as ModelLine;
            // BasisZ
            Plane zBaseP = doc.Application.Create.NewPlane(zVect, XYZ.BasisZ, paramPnt);
            SketchPlane zBasePSK = doc.FamilyCreate.NewSketchPlane(zBaseP);
            ModelLine zBaseLM = doc.FamilyCreate.NewModelCurve(zBaseL, zBasePSK) as ModelLine;
            // BasisY - 'true'
            Plane xzBaseP = doc.Application.Create.NewPlane(xzVec, XYZ.BasisZ, paramPnt);
            SketchPlane xzBasePSK = doc.FamilyCreate.NewSketchPlane(xzBaseP);
            ModelLine xzBaseLM = doc.FamilyCreate.NewModelCurve(xzBaseL, xzBasePSK) as ModelLine;
        }
    }
}
